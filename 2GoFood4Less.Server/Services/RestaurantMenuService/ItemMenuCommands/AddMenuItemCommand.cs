using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.MenuServices.MenuCommands
{
    public class AddMenuItemCommand : IMenuCommand
    {
        private readonly string _name;
        private readonly string _description;
        private readonly int _price;
        private readonly MenuItemPhoto _photo;

        public AddMenuItemCommand(string name, string description, int price, MenuItemPhoto photo)
        {
            _name = name;
            _description = description;
            _price = price;
            _photo = photo;
        }

        public async Task Execute(Menu menu, ApplicationDbContext db)
        {
            try
            {
                var newItem = new MenuItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _name,
                    Description = _description,
                    Price = _price,
                    Photo = _photo,
                    MenuId = menu.Id,
                    Menu = menu
                };

                menu.Items.Add(newItem);
                db.MenuItems.Add(newItem);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding menu item: {ex.Message}", ex);
            }
        }
    }
}
