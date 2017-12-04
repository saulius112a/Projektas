using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Models
{
    public class AccountInfoModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }

        public static explicit operator AccountInfoModel(AccountInfo a)
        {
            return new AccountInfoModel
            {
                Name = a.Name,
                LastName = a.LastName,
                Address = a.Address,
                Phone = a.Phone,
                ZipCode = a.ZipCode
            };
        }

        public static explicit operator AccountInfo(AccountInfoModel a)
        {
            return new AccountInfo
            {
                Name = a.Name,
                LastName = a.LastName,
                Address = a.Address,
                Phone = a.Phone,
                ZipCode = a.ZipCode
            };
        }
    }
}
