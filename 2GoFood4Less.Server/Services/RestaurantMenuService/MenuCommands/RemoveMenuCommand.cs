using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.MenuServices.MenuCommands
{
    public class RemoveMenuCommand : IMenuCommand
    {
        private readonly string _menuId;

        public RemoveMenuCommand(string menuId)
        {
            _menuId = menuId;
        }

        public async Task Execute(Menu unusedMenu, ApplicationDbContext db)
        {
            try
            {
                var menu = await db.Menus
                    .Include(m => m.Items)
                    .FirstOrDefaultAsync(m => m.Id == _menuId);

                if (menu == null) return;

                db.Menus.Remove(menu);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing menu: {ex.Message}", ex);
            }
        }
    }
}
