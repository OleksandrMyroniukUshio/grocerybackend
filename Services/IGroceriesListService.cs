using groceries_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace groceries_api.Services
{
    public interface IGroceriesListService
    {
        Task<IEnumerable<GroceryDTO>> GetGroceriesAsync();
        Task<GroceryDTO> AddGroceryAsync(string name, string type, bool isBought);
        Task<GroceryDTO> ChangeStateAsync(int id);
        Task<GroceryDTO> DeleteGroceryAsync(int id);
        Task ClearGroceriesAsync();
    }
}
