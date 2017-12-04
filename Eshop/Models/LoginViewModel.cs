using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class LogInViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public static explicit operator LogInViewModel(AccountModel a)
        {
            return new LogInViewModel
            {
                Email = a.Email,
                Password = a.Password
            };
        }
        public static explicit operator AccountModel(LogInViewModel l)
        {
            return new AccountModel
            {
                Email = l.Email,
                Password = l.Password
            };
        }
    }
}