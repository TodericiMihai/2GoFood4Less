using _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.MenuServices;
using _2GoFood4Less.Server.Services.MenuServices.MenuCommands;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Controllers.MenuControllerData
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/menu/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(string id)
        {
            try
            {
                var menu = await _menuService.GetByIdAsync(id);
                if (menu == null) return NotFound();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving menu: {ex.Message}");
            }
        }

        // GET: api/menu/restaurant/{id}
        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetAllMenusByRestaurant(string id)
        {
            try
            {
                var menu = await _menuService.GetAllByRestaurantIdAsync(id);
                if (menu == null) return NotFound();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving menu: {ex.Message}");
            }
        }

        // POST: api/menu/add
        [HttpPost("add")]
        public async Task<IActionResult> AddMenu([FromBody] AddMenuRequest request)
        {
            try
            {
                var command = new AddMenuCommand(request.Name, request.Description, request.RestaurantId);
                var menu = await _menuService.ExecuteCommandAsync(null, command);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding menu: {ex.Message}");
            }
        }

        // DELETE: api/menu/remove/{id}
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveMenu(string id)
        {
            try
            {
                var command = new RemoveMenuCommand(id);
                var menu = await _menuService.ExecuteCommandAsync(id, command);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error removing menu: {ex.Message}");
            }
        }
    }

}
