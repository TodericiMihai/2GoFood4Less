using _2GoFood4Less.Server.Models.RestaurantObjects;

namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto
{
    public static class DtoMapper
    {
        public static RestaurantObjectDto ToDto(Restaurant r)
        {
            return new RestaurantObjectDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                FoodType = r.FoodType,
                ManagerId = r.ManagerId,
                Menus = r.Menus?.Select(m => ToDto(m)).ToList() ?? new List<MenuObjectDto>()
            };
        }

        public static MenuObjectDto ToDto(Menu m)
        {
            return new MenuObjectDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Items = m.Items?.Select(i => new MenuItemObjectDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price
                }).ToList() ?? new List<MenuItemObjectDto>()
            };
        }
    }
}
