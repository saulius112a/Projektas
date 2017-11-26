using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class WishList:BaseEntity
    {
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        [Required]
        public virtual Account Account { get; set; }
        public virtual ICollection<WishListProduct> WishListProducts { get; set; }
    }
}
