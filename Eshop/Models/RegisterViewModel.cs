using Eshop.Data.Models;
using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }

        public static explicit operator AccountModel(RegisterViewModel r)
        {
            return new AccountModel
            {
                Email = r.Email,
                Password = r.Password,
                Role = 1,
                CreationDate = DateTime.Now,
                Status = false
            };
        }

        public static explicit operator AccountInfoModel(RegisterViewModel r)
        {
            return new AccountInfoModel
            {
                Name = r.Name,
                LastName = r.LastName,
                Address = r.Address,
                Phone = r.Phone,
                ZipCode = r.ZipCode
            };
        }
    }
}