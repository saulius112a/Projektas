using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual Entities.Attribute Attribute { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
