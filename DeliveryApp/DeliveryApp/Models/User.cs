using System;
using System.Collections.Generic;

namespace DeliveryApp.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPswd { get; set; }

        public User(string Email,string FirstName ,string LastName,string ps)
        {
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserPswd = ps;
            this.Id = 747474747;//when understand fix
        
        
        }
    }
}
