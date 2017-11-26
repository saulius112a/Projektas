using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class WishListProduct:BaseEntity
    {
        [Key, Column(Order = 1)]
        public int Importance { get; set; }
        public string Status { get; set; }
        [ForeignKey("WishList")]
        public int WishListId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public virtual WishList WishList { get; set; }
        [Required]
        public virtual Product Product { get; set; }
    }
}
