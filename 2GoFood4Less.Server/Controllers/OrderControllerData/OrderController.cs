using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.User.UserObjects;
using _2GoFood4Less.Server.Services.OrderService;
using _2GoFood4Less.Server.Services.OrderService.OrderCommands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Controllers.OrderControllerData
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly UserManager<Client> _userManager;

        public OrderController(OrderService orderService, UserManager<Client> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null) return NotFound();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving order: {ex.Message}");
            }
        }

        // POST: api/order/process/{id}
        [HttpPost("process/{id}")]
        public async Task<IActionResult> SetProcessing(string id)
        {
            try
            {
                var command = new SetOrderProcessingCommand();
                var order = await _orderService.ExecuteCommandAsync(id, command);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error setting order to Processing: {ex.Message}");
            }
        }

        // POST: api/order/finish/{id}
        [HttpPost("finish/{id}")]
        public async Task<IActionResult> SetFinished(string id)
        {
            try
            {
                var command = new SetOrderFinishedCommand();
                var order = await _orderService.ExecuteCommandAsync(id, command);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error setting order to Finished: {ex.Message}");
            }
        }

        // GET: api/order/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetClientOrders(string clientId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByClientAsync(clientId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving client orders: {ex.Message}");
            }
        }

        // GET: api/order/restaurant/{restaurantId}
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantOrders(string restaurantId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByRestaurantAsync(restaurantId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving restaurant orders: {ex.Message}");
            }
        }
    }
}
