using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.MenuServices.MenuCommands;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.MenuServices
{
    public class MenuItemService : IService<MenuItem, IMenuCommand>
    {
        private readonly ApplicationDbContext _db;

        public MenuItemService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Execute a command on a Menu (for adding/updating/removing items)
        public async Task<MenuItem> ExecuteCommandAsync(string menuId, IMenuCommand command)
        {
            if (string.IsNullOrEmpty(menuId))
                throw new ArgumentException("MenuId is required");

            // Load the parent menu
            var menu = await _db.Menus
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.Id == menuId);

            if (menu == null)
                throw new ArgumentException("Menu not found");

            // Execute the command on the menu
            await command.Execute(menu, _db);

            await _db.SaveChangesAsync();

            // Return the last item added (optional, only if command added an item)
            return menu.Items.LastOrDefault();
        }

        public async Task<MenuItem> GetByIdAsync(string id)
        {
            return await _db.MenuItems
                .Include(mi => mi.Menu)
                .ThenInclude(m => m.Items)
                .FirstOrDefaultAsync(mi => mi.Id == id);
        }

        public async Task<List<MenuItem>> GetAllByMenuIdAsync(string menuId)
        {
            return await _db.MenuItems
                .Where(mi => mi.MenuId == menuId)
                .ToListAsync();
        }
    }
}
