using PraeceptorCQRS.Domain.Email;

namespace PraeceptorCQRS.Application.Email
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        // Sending an Email in ASP.NET Core Asynchronously
        Task SendEmailAsync(Message message);
    }
}
