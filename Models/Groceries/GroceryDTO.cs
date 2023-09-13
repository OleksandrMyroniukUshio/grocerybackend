namespace groceries_api.Models.Groceries
{
    public class GroceryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsBought { get; set; }

        public GroceryDTO(int id, string name, string type, bool isBought)
        {
            Id = id;
            Name = name;
            Type = type;
            IsBought = isBought;
        }
    }
}
