using _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto;
using _2GoFood4Less.Server.Models.AuthObjects;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using _2GoFood4Less.Server.Services.RestaurantServices;
using _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        private readonly UserManager<Client> _userManager;

        public RestaurantController(RestaurantService restaurantService, UserManager<Client> userManager)
        {
            _restaurantService = restaurantService;
            _userManager = userManager;
        }

        // GET: api/restaurant/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant(string id)
        {
            try
            {
                var restaurant = await _restaurantService.GetByIdAsync(id);
                if (restaurant == null) return NotFound();
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving restaurant: {ex.Message}");
            }
        }

        // POST: api/restaurant/add
        [HttpPost("add")]
        public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest request)
        {
            try
            {
                var command = new AddRestaurantCommand(
                    request.Name,
                    request.Description,
                    request.FoodType,
                    request.Photo,
                    request.OpenTime,
                    request.ClosingTime
                );

                var restaurant = await _restaurantService.ExecuteCommandAsync(null, command);
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding restaurant: {ex.Message}");
            }
        }

        // DELETE: api/restaurant/remove/{id}
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveRestaurant(string id)
        {
            try
            {
                var command = new RemoveRestaurantCommand(id);
                var restaurant = await _restaurantService.ExecuteCommandAsync(id, command);
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error removing restaurant: {ex.Message}");
            }
        }
    }

}
