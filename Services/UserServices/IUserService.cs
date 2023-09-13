using groceries_api.Models.Users;

namespace groceries_api.Services.UserServices
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string username, string password);
        Task<User> AuthenticateAsync(string username, string password);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
    }

}
