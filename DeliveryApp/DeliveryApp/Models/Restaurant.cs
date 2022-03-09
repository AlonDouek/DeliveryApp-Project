using System;
using System.Collections.Generic;


namespace DeliveryApp.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Menus = new HashSet<Menu>();
        }

        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResAddress { get; set; }
        public string Image { get; set; }
        public TimeSpan OpeningHours { get; set; }
        public TimeSpan ClosingHours { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
