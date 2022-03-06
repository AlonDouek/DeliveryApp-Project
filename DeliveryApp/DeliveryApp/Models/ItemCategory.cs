using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace DeliveryApp.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
