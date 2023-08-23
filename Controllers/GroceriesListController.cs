using groceries_api.Models;
using groceries_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace groceries_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroceriesListController : ControllerBase
    {
        private readonly IGroceriesListService _groceriesListService;
        public GroceriesListController(IGroceriesListService groceriesListService)
        {
            _groceriesListService = groceriesListService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceryDTO>>> GetGroceriesAsync()
        {
            var groceries = await _groceriesListService.GetGroceriesAsync();
            return Ok(groceries);
        }
        [HttpPost]
        public async Task<ActionResult<GroceryDTO>> AddGroceryAsync([FromBody] GroceryCreateModel grocery)
        {
            var createdGrocery = await _groceriesListService.AddGroceryAsync(grocery.Name, grocery.Type, grocery.IsBought);
            return Ok(createdGrocery);
        }
        [HttpPut]
        public async Task<ActionResult<GroceryDTO>> ChangeStateAsync([FromBody] int id)
        {
            var grocery = await _groceriesListService.ChangeStateAsync(id);
            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroceryDTO>> DeleteGroceryAsync (int id)
        {
            var grocery = await _groceriesListService.DeleteGroceryAsync(id);

            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }
        [HttpDelete]
        public async Task<ActionResult> ClearGroceriesListAsync()
        {
            await _groceriesListService.ClearGroceriesAsync();
            return Ok();
        }
    }
}
