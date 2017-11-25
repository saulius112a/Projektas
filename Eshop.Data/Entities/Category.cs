﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<Product> Product { get; set; }

    }
}
