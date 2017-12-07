using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Eshop.Data.Entities.Account;

namespace Eshop.Data.Models
{


    public class AccountModel
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccRole Role { get; set; }
        public DateTime CreationDate { get; set; }
        public AccStatus Status { get; set; }
        //public virtual ICollection<LoginLog> LoginLogs { get; set; }
        //public virtual AccountInfo AccountInfo { get; set;}

        public static explicit operator AccountModel(Account a)
        {
            return new AccountModel
            {
                Id = a.Id,
                Email = a.Email,
                Password = a.Password,
                Role = a.Role,
                CreationDate = a.CreationDate,
                Status = a.Status,
                //LoginLogs = a.LoginLogs,
                //AccountInfo = a.AccountInfo
            };
        }

        public static explicit operator Account(AccountModel a)
        {
            return new Account
            {
                Id = a.Id,
                Email = a.Email,
                Password = a.Password,
                Role = a.Role,
                CreationDate = a.CreationDate,
                Status = a.Status,
                //LoginLogs = a.LoginLogs,
                //AccountInfo = a.AccountInfo
            };
        }
    }
}
