using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;
using Microsoft.EntityFrameworkCore;

namespace _2GoFood4Less.Server.Services.CartServices.CartCalcualtion
{
 
    public class CartCalculator : ICartCalculator
    {
        public async Task UpdateCartValue(Cart cart, ApplicationDbContext db)
        {
            cart.Value = await db.CartItems
                .Where(ci => ci.CartId == cart.Id)
                .SumAsync(ci => ci.Quantity * ci.MenuItem.Price);
        }
    }

}
