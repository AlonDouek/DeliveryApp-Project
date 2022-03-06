using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryApp.DTO
{
    class ChangeDTO
    {
        public User Nuser;
        string CuserEmail;

        
        public ChangeDTO(string u1E, User u2)
        {
            CuserEmail = u1E;
            Nuser = new User {
                Email = u2.Email,
                Password = u2.Password,
                Username = u2.Username,
                Address = u2.Address,
                CreditCard = u2.CreditCard,
                PhoneNumber = u2.PhoneNumber
            };
        }
    }
}
