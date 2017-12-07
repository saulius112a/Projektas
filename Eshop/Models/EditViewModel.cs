using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class EditViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }

        public static explicit operator AccountInfoModel(EditViewModel r)
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