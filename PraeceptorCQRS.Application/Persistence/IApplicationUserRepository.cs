namespace PraeceptorCQRS.Application.Persistence
{
    public interface IApplicationUserRepository
    {
        Task<List<Domain.Entities.ApplicationUser>> QueryDefault(string sql);
        Task<bool> Exists(string username);
        Task<int> GetUsersCount();
        Task<Domain.Entities.ApplicationUser?> GetApplicationUserByUserName(string username);
        Task<Domain.Entities.ApplicationUser?> GetApplicationUserById(Guid id);
        Task<Domain.Entities.ApplicationUser?> CreateApplicationUser(Domain.Entities.ApplicationUser user);
        Task UpdateApplicationUser(Domain.Entities.ApplicationUser user);
        Task DeleteApplicationUser(Guid id);
        Task<int> GetApplicationUsersCountByInstitute(Guid instituteId);
        Task<List<Domain.Entities.ApplicationUser>> GetApplicationUserPage(
            int Start,
            int Count,
            string? Sort,
            bool Ascending,
            string? HoldingIdFilter,
            string? InstituteIdFilter,
            string? CourseIdFilter,
            string? UserNameFilter,
            string? EmailFilter,
            string? PhoneNumberFilter,
            bool? EnabledFilter,
            char? GenderFilter
            );
    }
}
