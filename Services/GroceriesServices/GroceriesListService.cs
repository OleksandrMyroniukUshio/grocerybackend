using groceries_api.Database;
using groceries_api.Models.Groceries;
using groceries_api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace groceries_api.Services.GroceriesServices
{
    public class GroceriesListService : IGroceriesListService
    {
        private readonly GroceriesDbContext _dbcontext;
        public GroceriesListService(GroceriesDbContext dbContext)
        {
            _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<GroceryDTO>> GetGroceriesAsync(int userId)
        {
            var groceryDTOs = await _dbcontext.Groceries
            .Where(g => g.UserId == userId)
            .OrderBy(g => g.Id)
            .Select(g => new GroceryDTO(g.Id, g.Name, g.Type, g.IsBought))
            .ToListAsync();
            return groceryDTOs;
        }
        public async Task<GroceryDTO> AddGroceryAsync(string name, string type, bool isBought, int userId)
        {
            var grocery = new Grocery(name, type, isBought, userId);
            await _dbcontext.Groceries.AddAsync(grocery);
            await _dbcontext.SaveChangesAsync();

            return new GroceryDTO(grocery.Id, grocery.Name, grocery.Type, grocery.IsBought);
        }
        public async Task<GroceryDTO> ChangeStateAsync(int id, int userId)
        {
            var grocery = await _dbcontext.Groceries.Where(g => g.UserId == userId).FirstOrDefaultAsync(g => g.Id == id);
            if (grocery != null)
            {
                grocery.IsBought = !grocery.IsBought;
                await _dbcontext.SaveChangesAsync();
                return new GroceryDTO(grocery.Id, grocery.Name, grocery.Type, grocery.IsBought);
            }
            return null;

        }
        public async Task<GroceryDTO> DeleteGroceryAsync(int id, int userId)
        {
            var grocery = await _dbcontext.Groceries.Where(g => g.UserId == userId).FirstOrDefaultAsync(g => g.Id == id);
            if (grocery != null)
            {
                _dbcontext.Groceries.Remove(grocery);
                await _dbcontext.SaveChangesAsync();

                return new GroceryDTO(grocery.Id, grocery.Name, grocery.Type, grocery.IsBought);
            }
            return null;
        }
        public async Task ClearGroceriesAsync(int userId)
        {
            var userGroceries = _dbcontext.Groceries.Where(g => g.UserId == userId);
            _dbcontext.Groceries.RemoveRange(userGroceries);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
