using _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using _2GoFood4Less.Server.Services.MenuServices;
using _2GoFood4Less.Server.Services.MenuServices.MenuCommands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Controllers.MenuControllerData
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly MenuItemService _menuItemService;

        public MenuItemController(MenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/menuitem/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItem(string id)
        {
            try
            {
                var item = await _menuItemService.GetByIdAsync(id);
                if (item == null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving menu item: {ex.Message}");
            }
        }

        // POST: api/menuitem/add/{menuId}
        [HttpPost("add/{menuId}")]
        public async Task<IActionResult> AddMenuItem(string menuId, [FromBody] AddMenuItemRequest request)
        {
            try
            {
                var command = new AddMenuItemCommand(request.Name, request.Description, request.Price, request.Photo);
                var item = await _menuItemService.ExecuteCommandAsync(menuId, command);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding menu item: {ex.Message}");
            }
        }

        // DELETE: api/menuitem/remove/{menuId}/{itemId}
        [HttpDelete("remove/{menuId}/{itemId}")]
        public async Task<IActionResult> RemoveMenuItem(string menuId, string itemId)
        {
            try
            {
                var command = new RemoveMenuItemCommand(itemId);
                var item = await _menuItemService.ExecuteCommandAsync(menuId, command);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error removing menu item: {ex.Message}");
            }
        }
    }

}
