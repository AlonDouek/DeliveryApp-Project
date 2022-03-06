using System;
using System.Collections.Generic;


namespace DeliveryApp.Models
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int MenuItemId { get; set; }
        public int MenuId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual ItemCategory Category { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
