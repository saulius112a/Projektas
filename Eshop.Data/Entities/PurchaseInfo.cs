using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class PurchaseInfo:BaseEntity
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Price { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Purchase")]
        public int PurchaseId { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        [Required]
        public virtual Purchase Purchase { get; set; }
    }
}
