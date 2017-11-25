using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class TraitValue
    {
        [Key]
        [ForeignKey("ProductAttribute")]
        public int Id { get; set; }

        [Required]
        public virtual ProductAttribute ProductAttribute { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
