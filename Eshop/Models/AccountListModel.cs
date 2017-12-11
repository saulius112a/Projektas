using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Models
{
    public class AccountListModel
    {
        [Display(Name = "Sukurta nuo")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "Sukurta iki")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public List<Data.Entities.Account> Accounts { get; set; }
    }
}