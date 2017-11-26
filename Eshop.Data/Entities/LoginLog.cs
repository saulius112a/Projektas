using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class LoginLog:BaseEntity
    {
        [Required]
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public virtual Account Account { get; set; }
        
    }
}
