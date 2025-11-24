using _2GoFood4Less.Server.Controllers.CartControllerData.CartDto;
using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.AuthObjects;
using _2GoFood4Less.Server.Services.CartServices;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;
using _2GoFood4Less.Server.Services.CartServices.CartCommands;
using _2GoFood4Less.Server.Services.CartServices.CartCommands.CartCommandsDto;
using _2GoFood4Less.Server.Services.OrderService;
using _2GoFood4Less.Server.Services.OrderService.OrderCommands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Controllers.CartControllerData
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly UserManager<AppUser> _userManager;
 
        public CartController(CartService cartService, UserManager<AppUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
            
        }

        // GET: api/cart/{clientId}
        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetCart(string clientId)
        {
            try
            {
                var cart = await _cartService.GetCartAsync(clientId);
                if (cart == null) return NotFound();
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
        {
            try
            {
                var command = new AddItemCommand(request.MenuItemId, request.Quantity);
                var cart = await _cartService.ExecuteCommandAsync(request.ClientId, command);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/cart/remove
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveItemRequest request)
        {
            try
            {
                var command = new RemoveItemCommand(request.MenuItemId);
                var cart = await _cartService.ExecuteCommandAsync(request.ClientId, command);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/cart/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityRequest request)
        {
            try
            {
                var command = new UpdateQuantityCommand(request.MenuItemId, request.NewQuantity);
                var cart = await _cartService.ExecuteCommandAsync(request.ClientId, command);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/cart/checkout/{clientId}
        [HttpPost("checkout/{clientId}")]
        public async Task<IActionResult> Checkout(string clientId,OrderLocationPayment dto)
        {
            try
            {

                var command = new CreateOrderFromCartCommand(dto);
                var cart = await _cartService.ExecuteCommandAsync(clientId, command);

                if (cart == null) return BadRequest("Cart is empty or invalid.");

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/cart/clear/{clientId}
        [HttpPost("clear/{clientId}")]
        public async Task<IActionResult> ClearCart(string clientId)
        {
            try
            {
                var command = new ClearCartCommand();
                var cart = await _cartService.ExecuteCommandAsync(clientId, command);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
