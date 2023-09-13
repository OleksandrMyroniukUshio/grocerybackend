using System.ComponentModel.DataAnnotations;
using groceries_api.Models.Users;
namespace groceries_api.Models.Groceries
{
    public class Grocery
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsBought { get; set; }
        public DateTime? Created { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } 
        public Grocery(string name, string type, bool isBought, int userId)
        {
            Name = name;
            Type = type;
            IsBought = isBought;
            Created = DateTime.UtcNow;
            UserId = userId;
        }
    }

}
