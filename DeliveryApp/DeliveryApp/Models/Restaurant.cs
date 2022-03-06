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
        public int Description { get; set; }
        public TimeSpan OpeningHours { get; set; }
        public TimeSpan ClosingHours { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
