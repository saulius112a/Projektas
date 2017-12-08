using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Measurement
    {
        [Key]
        [ForeignKey("ProductAttribute")]
        public int ProductAttributeId { get; set; }
        public string Unit { get; set; }
        [Required]
        public double Value { get; set; }
        //[Key]
        [Required]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
