using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class CartInfo:BaseEntity
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        [Required]
        public virtual Cart Cart { get; set; }
    }
}
