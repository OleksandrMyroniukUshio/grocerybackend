using groceries_api.Database;
using groceries_api.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace groceries_api.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly GroceriesDbContext _context;

        public UserService(GroceriesDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = Encoding.UTF8.GetBytes(hashedPassword)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(password, Encoding.UTF8.GetString(user.PasswordHash)))
                return null;

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }

}
