using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class AccountInfo
    {
        [Display(Name = "Vardas")]
        public string Name { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Display(Name = "Adresas")]
        public string Address { get; set; }

        [Display(Name = "Tel. numeris")]
        public string Phone { get; set; }

        [Display(Name = "Pašto kodas")]
        public string ZipCode { get; set; }

        [Key,ForeignKey("Account")]
        public int AccountId { get; set; }

        [Required]
        public virtual Account Account { get; set; }
    }
}
