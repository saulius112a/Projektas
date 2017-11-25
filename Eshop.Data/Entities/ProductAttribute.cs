using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class ProductAttribute:BaseEntity
    {
        [Required]
        public virtual Attribute Attribute { get; set; }
        public virtual Measurement Measurement { get; set; }
        public virtual TraitValue TraitValue { get; set; }
        public virtual Product Product { get; set; }

    }
}
