using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

using Serilog;

using System.Diagnostics;
using System.Linq;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class ApplicationUserRepository : AbstractRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<int> GetUsersCount()
            => await Count(o => true);

        public async Task<ApplicationUser?> CreateApplicationUser(ApplicationUser user)
        {
            // var secret = new Secret(user.PasswordHash);

            return await CreateDefault(user);
        }

        public async Task<bool> Exists(string username)
            => await GetApplicationUserByUserName(username) is not null;
        public async Task<ApplicationUser?> GetApplicationUserByUserName(string username)
            => await ReadDefault(o => o.UserName == username);
        public async Task<ApplicationUser?> GetApplicationUserById(Guid id)
            => await ReadDefault(o => Guid.Parse(o.Id) == id);

        public async Task UpdateApplicationUser(ApplicationUser user)
        {
            DetachLocal(o => o.Id == user.Id);
            await UpdateDefault(user);
        }

        public async Task DeleteApplicationUser(Guid id)
        {
            var user = await ReadDefault(o => Guid.Parse(o.Id) == id);

            if (user is not null)
                await DeleteDefault(user);
        }

        public async Task<int> GetApplicationUsersCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId.ToString());

        public async Task<List<ApplicationUser>> GetApplicationUserPage(
            int start,
            int count,
            string? sort,
            bool ascending,
            string? holdingIdFilter,
            string? instituteIdFilter,
            string? courseIdFilter,
            string? userNameFilter,
            string? emailFilter,
            string? phoneNumberFilter,
            bool? enabledFilter,
            char? genderFilter
            )
        {
            var list = await ListDefault(o => true);
            var filteredList = new List<ApplicationUser>();
            var isFiltered = false;

            foreach (var entity in list)
            {
                if (!string.IsNullOrWhiteSpace(userNameFilter))
                {
                    isFiltered = true;

                    if (!string.IsNullOrWhiteSpace(entity.UserName) && entity.UserName.Contains(userNameFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(emailFilter))
                {
                    isFiltered = true;

                    if (!string.IsNullOrWhiteSpace(entity.Email) && entity.Email.Contains(emailFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(holdingIdFilter))
                {
                    isFiltered = true;

                    if (!string.IsNullOrWhiteSpace(entity.HoldingId) && entity.HoldingId.Contains(holdingIdFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(instituteIdFilter))
                {
                    isFiltered = true;

                    if (!string.IsNullOrWhiteSpace(entity.InstituteId) && entity.InstituteId.Contains(instituteIdFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(courseIdFilter))
                {
                    isFiltered = true;

                    if (!string.IsNullOrWhiteSpace(entity.CourseId) && entity.CourseId.Contains(courseIdFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(phoneNumberFilter))
                {
                    isFiltered = true;

                    if (!string.IsNullOrWhiteSpace(entity.PhoneNumber) && entity.PhoneNumber.Contains(phoneNumberFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (enabledFilter is not null)
                {
                    isFiltered = true;

                    if (entity.IsEnabled == enabledFilter)
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (genderFilter is not null && "MmFf".Contains((char)genderFilter))
                {
                    isFiltered = true;

                    if (entity.Gender == genderFilter)
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
            }

            if (isFiltered)
                list = SortBy(filteredList, sort, ascending);
            else
                list = SortBy(list, sort, ascending);

            if (list.Count > start + count)
                return list.GetRange(start, count);
            else if (list.Count <= count || count == -1)
                return list;
            else
                return list.GetRange(start, list.Count - start);
        }

        private static List<ApplicationUser> SortBy(List<ApplicationUser> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "UserName" => Global.SortList(list, x => x.UserName, ascending),
                "Email" => Global.SortList(list, x => x.Email, ascending),
                "HoldingId" => Global.SortList(list, x => x.HoldingId, ascending),
                "InstituteId" => Global.SortList(list, x => x.InstituteId, ascending),
                "CourseId" => Global.SortList(list, x => x.CourseId, ascending),
                "Gender" => Global.SortList(list, x => x.Gender, ascending),
                _ => list
            };
        }
    }
}
