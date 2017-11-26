using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class CategoryAttribute:BaseEntity
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }
        [Required]
        public virtual Category Category{get;set;}
        [Required]
        public virtual Attribute Attribute { get; set; }
    }
}
