using groceries_api.Database;
using groceries_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace groceries_api.Services
{
    public class GroceriesListService : IGroceriesListService
    {
        private readonly GroceriesDbContext _dbcontext;
        public GroceriesListService(GroceriesDbContext dbContext)
        {
            _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<GroceryDTO>> GetGroceriesAsync()
        {
            var groceryDTOs = await _dbcontext.Groceries
            .OrderBy(g => g.Id)
            .Select(g => new GroceryDTO(g.Id, g.Name, g.Type, g.IsBought))
            .ToListAsync();
            return groceryDTOs;
        }
        public async Task<GroceryDTO> AddGroceryAsync(string name, string type, bool isBought)
        {
            var grocery = new Grocery(name,type,isBought);
            await _dbcontext.Groceries.AddAsync(grocery);
            await _dbcontext.SaveChangesAsync();
            
            return new GroceryDTO(grocery.Id, grocery.Name, grocery.Type, grocery.IsBought);
        }
        public async Task<GroceryDTO> ChangeStateAsync(int id)
        {
            var grocery = await _dbcontext.Groceries.FindAsync(id);
            if (grocery != null) { 
                grocery.IsBought = !grocery.IsBought;
                await _dbcontext.SaveChangesAsync();
                return new GroceryDTO(grocery.Id, grocery.Name, grocery.Type, grocery.IsBought);
            }
            return null;

        }
        public async Task<GroceryDTO> DeleteGroceryAsync(int id)
        {
            var grocery = await _dbcontext.Groceries.FindAsync(id);
            if (grocery != null)
            {
                _dbcontext.Groceries.Remove(grocery);
                await _dbcontext.SaveChangesAsync();

                return new GroceryDTO(grocery.Id, grocery.Name, grocery.Type, grocery.IsBought);
            }
            return null;
        }
        public async Task ClearGroceriesAsync()
        {
            await _dbcontext.Database.ExecuteSqlRawAsync("delete from \"groceries\"");
        }
    }
}
