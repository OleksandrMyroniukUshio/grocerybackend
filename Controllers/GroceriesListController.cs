using groceries_api.Models.Groceries;
using groceries_api.Services.GroceriesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace groceries_api.Controllers
{
    [Authorize]
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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var groceries = await _groceriesListService.GetGroceriesAsync(userId);
            return Ok(groceries);
        }
        [HttpPost]
        public async Task<ActionResult<GroceryDTO>> AddGroceryAsync([FromBody] GroceryCreateModel grocery)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var createdGrocery = await _groceriesListService.AddGroceryAsync(grocery.Name, grocery.Type, grocery.IsBought, userId);
            return Ok(createdGrocery);
        }
        [HttpPut]
        public async Task<ActionResult<GroceryDTO>> ChangeStateAsync([FromBody] int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var grocery = await _groceriesListService.ChangeStateAsync(id, userId);
            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroceryDTO>> DeleteGroceryAsync (int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var grocery = await _groceriesListService.DeleteGroceryAsync(id, userId);

            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }
        [HttpDelete]
        public async Task<ActionResult> ClearGroceriesListAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _groceriesListService.ClearGroceriesAsync(userId);
            return Ok();
        }
    }
}
