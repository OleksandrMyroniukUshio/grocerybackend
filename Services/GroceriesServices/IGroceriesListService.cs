using groceries_api.Models.Groceries;
using Microsoft.AspNetCore.Mvc;

namespace groceries_api.Services.GroceriesServices
{
    public interface IGroceriesListService
    {
        Task<IEnumerable<GroceryDTO>> GetGroceriesAsync(int userId);
        Task<GroceryDTO> AddGroceryAsync(string name, string type, bool isBought, int userId);
        Task<GroceryDTO> ChangeStateAsync(int id, int userId);
        Task<GroceryDTO> DeleteGroceryAsync(int id, int userId);
        Task ClearGroceriesAsync(int userId);
    }
}
