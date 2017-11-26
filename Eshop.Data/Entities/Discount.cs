using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Discount
    {
        [Required]
        public double Value { get; set; }
        public string Name { get; set; }
        [Key,ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public virtual Product Product { get; set; }
    }
}
