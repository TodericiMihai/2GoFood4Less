using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.MenuServices.MenuCommands
{
    public class RemoveMenuItemCommand : IMenuCommand
    {
        private readonly string _menuItemId;

        public RemoveMenuItemCommand(string menuItemId)
        {
            _menuItemId = menuItemId;
        }

        public async Task Execute(Menu menu, ApplicationDbContext db)
        {
            try
            {
                var item = menu.Items.FirstOrDefault(mi => mi.Id == _menuItemId);
                if (item == null) return;

                menu.Items.Remove(item);
                db.MenuItems.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing menu item: {ex.Message}", ex);
            }
        }
    }
}
