using System;
using System.Collections.Generic;


namespace DeliveryServer.Models
{
    public partial class OrderItem
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Amount { get; set; }

        public virtual MenuItem MenuItem { get; set; }
    }
}
