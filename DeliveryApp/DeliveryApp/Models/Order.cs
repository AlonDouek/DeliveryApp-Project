using System;
using System.Collections.Generic;


namespace DeliveryServer.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }

        public virtual OrderStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}
