using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.MenuServices.MenuCommands;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.MenuServices
{
    public class MenuService : IService<Menu,IMenuCommand>
    {
        private readonly ApplicationDbContext _db;

        public MenuService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Menu> ExecuteCommandAsync(string id, IMenuCommand command)
        {
            Menu menu = null;
            if (!string.IsNullOrEmpty(id))
            {
                menu = await GetByIdAsync(id);
            }

            await command.Execute(menu, _db);
            await _db.SaveChangesAsync();

            return menu;
        }

        public async Task<Menu> GetByIdAsync(string id)
        {
            return await _db.Menus
                .Include(m => m.Items)
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Menu>> GetAllByRestaurantIdAsync(string restaurantId)
        {
            return await _db.Menus
                .Include(m => m.Items)
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();
        }
    }
}
