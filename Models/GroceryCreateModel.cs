namespace groceries_api.Models
{
    public class GroceryCreateModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsBought { get; set; }

        public GroceryCreateModel(string name, string type, bool isBought)
        {
            Name = name;
            Type = type;
            IsBought = isBought;
        }
    }
}
