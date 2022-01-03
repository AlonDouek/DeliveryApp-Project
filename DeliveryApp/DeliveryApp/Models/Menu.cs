using System;
using System.Collections.Generic;


namespace DeliveryServer.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuCatagories = new HashSet<MenuCatagory>();
            Restaurants = new HashSet<Restaurant>();
        }

        public int MenuId { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<MenuCatagory> MenuCatagories { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
