using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Discount:BaseEntity
    {
        [Required]
        public double Value { get; set; }
        public string Name { get; set; }
        [Required]
        public virtual Product Product { get; set; }
    }
}
