using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Attribute :BaseEntity
    {
        [MaxLength(32)]
        public string Name { get; set; }
        public bool IsTrait { get; set; }
        [MaxLength(32)]
        public string Description { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

    }
}
