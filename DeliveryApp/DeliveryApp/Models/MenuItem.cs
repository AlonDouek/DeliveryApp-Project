using System;
using System.Collections.Generic;


namespace DeliveryServer.Models
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int MenuItemId { get; set; }
        public int CatagoryId { get; set; }
        public string Name { get; set; }
        public int Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual MenuCatagory Catagory { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
