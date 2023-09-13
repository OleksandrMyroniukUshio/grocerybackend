using groceries_api.Models.Groceries;
using Microsoft.AspNetCore.Identity;

namespace groceries_api.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public virtual ICollection<Grocery> Groceries { get; set; }
    }

}
