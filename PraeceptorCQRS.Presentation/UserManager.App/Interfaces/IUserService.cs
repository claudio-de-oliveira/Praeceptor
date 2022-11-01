using PraeceptorCQRS.Contracts.Entities.User;

using UserManager.App.Models;

namespace UserManager.App.Interfaces
{
    public interface IUserService
    {
        Task<int> GetUserCount(Guid instituteId);
        Task<HttpResponseMessage> PostPage(GetUserPageRequest request);
        Task<UserModel?> GetUserById(Guid id);
        Task<HttpResponseMessage> UpdateUser(UpdateUserRequest request);
        Task<HttpResponseMessage> CreateUser(CreateUserRequest request);
        Task<HttpResponseMessage> DeleteUser(Guid id);
    }
}
