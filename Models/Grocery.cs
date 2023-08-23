using System.ComponentModel.DataAnnotations;
namespace groceries_api.Models
{
    public class Grocery
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsBought { get; set; }
        public DateTime? Created { get; set;}
        public Grocery(string name, string type, bool isBought)
        {
            Name = name;
            Type = type;
            IsBought = isBought;
            Created = DateTime.UtcNow;
        }
    }

}
