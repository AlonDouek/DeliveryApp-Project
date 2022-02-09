using System;
using System.Collections.Generic;


namespace DeliveryServer.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCard { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public User(string Email, string UN, string ps, string Address, string PhoneNumber, string CreditCard)
        {
            this.Email = Email;
            this.Username = UN;
            this.Password = ps;
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
            this.CreditCard = CreditCard;

        }

        public User(User u)
        {
            this.Email = u.Email;
            this.Username = u.Username;
            this.Password = u.Password;
            this.Address = u.Address;
            this.PhoneNumber = u.PhoneNumber;
            this.CreditCard = u.CreditCard;

        }
    }
}
