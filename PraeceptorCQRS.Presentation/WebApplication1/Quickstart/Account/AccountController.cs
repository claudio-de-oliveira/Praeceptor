// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Ardalis.GuardClauses;

using IdentitiServer.Api.Models;

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PasswordGenerator;

using PraeceptorCQRS.Application.Email;
using PraeceptorCQRS.Contracts.Entities.Admin;
using PraeceptorCQRS.Domain.Email;
using PraeceptorCQRS.Domain.Entities;

using Serilog;

namespace IdentityServer.Api.Quickstart.UI
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IEmailSender emailSender,
            IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _events = events;
            _emailSender = emailSender;
        }

        #region Register
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await Task.CompletedTask;

            // build a model so we know what to show on the register page
            var vm = BuildRegistrationViewModelAsync();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                EmailConfirmed = false,
                Gender = userModel.Gender,
                IsEnabled = true,
                NormalizedEmail = userModel.Email.ToUpper(),
                NormalizedUserName = userModel.UserName.ToUpper(),
                PhoneNumber = userModel.PhoneNumber,
                PasswordHash = userModel.Password,
                HoldingId = Guid.Empty.ToString(),
                InstituteId = Guid.Empty.ToString(),
                CourseId = Guid.Empty.ToString()
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            Guard.Against.Null(confirmationLink);
            var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null!);
            await _emailSender.SendEmailAsync(message);

            await _userManager.AddToRoleAsync(user, "Visitor");
            // await _userManager.AddToRoleAsync(user, userModel.Role);
            // return RedirectToAction(nameof(HomeController.Index), "Home");

            return RedirectToAction(nameof(SuccessRegistration));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("AccessDenied");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return View(result.Succeeded ? nameof(ConfirmEmail) : "AccessDenied");
        }

        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }
        #endregion

        #region Login
        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null!)
        {
            if (returnUrl is null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }
            else
            {
                // build a model so we know what to show on the login page
                var vm = await BuildLoginViewModelAsync(returnUrl);
                return View(vm);
            }
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && user.IsEnabled)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                        if (context != null)
                        {
                            if (context.IsNativeClient())
                            {
                                // The client is native, so this change in how to
                                // return the response is for better UX for the end user.
                                return this.LoadingPage("Redirect", model.ReturnUrl);
                            }
                            // if (await _clientStore.IsPkceClientAsync(context.ClientId))
                            // {
                            //     // if the client is PKCE then we assume it's native, so this change in how to
                            //     // return the response is for better UX for the end user.
                            //     return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
                            // }

                            // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                            return Redirect(model.ReturnUrl);
                        }

                        // request for a local page
                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                            // return Redirect("~/");
                        }
                        else
                        {
                            // user might have clicked on a malicious link - should be logged
                            throw new Exception("invalid return URL");
                        }
                    }
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "credenciais inválidas", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        #region Logout
        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            Log.Information("***************** F A Z E N D O   L O G O U T *****************");
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            Guard.Against.Null(User.Identity);
            if (User.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            return View("LoggedOut", vm);
        }
        #endregion

        #region Forgot Password
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordModel { Email = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);

            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            Guard.Against.Null(callback);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null!);
            await _emailSender.SendEmailAsync(message);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));
            Guard.Against.Null(user);
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region Cadastro do administrador
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AdminRegister([FromBody] CreateAdminRequest request)
        {
            var pwd = new Password(128).IncludeLowercase().IncludeUppercase().IncludeNumeric().IncludeSpecial("[]{}^_=");
            var password = pwd.Next();

            var holdingId = Guid.Empty.ToString();
            var instituteId = Guid.Empty.ToString();
            var courseId = Guid.Empty.ToString();

            IList<ApplicationUser> administrators = await _userManager.GetUsersInRoleAsync(request.Role);
            ApplicationUser? admin;

            if (string.Compare(request.Role, "HoldingAdmin") == 0)
            {
                if (string.IsNullOrWhiteSpace(request.Id) || string.Compare(request.Id, Guid.Empty.ToString()) == 0)
                    throw new InvalidDataException("HoldingId não pode ser nulo ou vazio!");
                holdingId = request.Id;
                admin = administrators.FirstOrDefault(o => o.HoldingId == holdingId);
            }
            else if (string.Compare(request.Role, "InstituteAdmin") == 0)
            {
                if (string.IsNullOrWhiteSpace(request.Id) || string.Compare(request.Id, Guid.Empty.ToString()) == 0)
                    throw new InvalidDataException("InstituteId não pode ser nulo ou vazio!");
                instituteId = request.Id;
                admin = administrators.FirstOrDefault(o => o.InstituteId == instituteId);
            }
            else if (string.Compare(request.Role, "CourseAdmin") == 0)
            {
                if (string.IsNullOrWhiteSpace(request.Id) || string.Compare(request.Id, Guid.Empty.ToString()) == 0)
                    throw new InvalidDataException("CourseId não pode ser nulo ou vazio!");
                courseId = request.Id;
                admin = administrators.FirstOrDefault(o => o.CourseId == courseId);
            }
            else
            {
                throw new InvalidDataException("Role desconhecida.");
            }

            if (admin is not null)
            {
                await _userManager.DeleteAsync(admin);
            }

            var userModel = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = false,
                Gender = request.Gender,
                IsEnabled = true,
                NormalizedEmail = request.Email.ToUpper(),
                NormalizedUserName = request.UserName.ToUpper(),
                PhoneNumber = request.PhoneNumber,
                PasswordHash = password,
                HoldingId = holdingId,
                InstituteId = instituteId,
                CourseId = courseId
            };

            var result = await _userManager.CreateAsync(userModel, password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userModel);
            var confirmationLink = Url.Action(nameof(AdminConfirmEmail), "Account", new { token, email = userModel.Email, temporary_password = password }, Request.Scheme);
            Guard.Against.Null(confirmationLink);
            var message = new Message(new string[] { userModel.Email }, "Confirme seu registro no link abaixo", confirmationLink, null!);
            await _emailSender.SendEmailAsync(message);

            await _userManager.AddToRoleAsync(userModel, request.Role);

            return RedirectToAction(nameof(SuccessAdminRegistration));
        }

        [HttpGet]
        public IActionResult SuccessAdminRegistration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminConfirmEmail(string token, string email, string temporary_password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("AccessDenied");

            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, temporary_password);

            if (!checkPasswordResult)
            {
                return View("NoMoreValidEmailVerification");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return View("InvalidPasswordConfirmation");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var model = new AdminResetPasswordModel { Token = resetToken, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminResetPassword(AdminResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(AdminResetPasswordConfirmation));

            Guard.Against.Null(user);
            var tok = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, tok, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(AdminResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult AdminResetPasswordConfirmation()
        {
            return View();
        }
        #endregion

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helper APIs for the AccountController
        private static UserRegistrationModel BuildRegistrationViewModelAsync()
            => new()
            {
                UserName = "",
                Email = "",
                Gender = ' ',
                Password = "",
                ConfirmPassword = "",
                HoldingId = null!,
                InstituteId = null!,
                CourseId = null!,
                PhoneNumber = null!
            };

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context.LoginHint,
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            Guard.Against.Null(User?.Identity);
            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout.ClientName) ? logout.ClientId : logout.ClientName,
                SignOutIframeUrl = logout.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            return vm;
        }
        #endregion
    }
}
