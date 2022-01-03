using System;
using System.Collections.Generic;


namespace DeliveryServer.Models
{
    public partial class MenuCatagory
    {
        public MenuCatagory()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int CatagoryId { get; set; }
        public int MenuId { get; set; }
        public int Name { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
