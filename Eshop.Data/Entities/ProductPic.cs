﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class ProductPic:BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconLocation { get; set; }
        public virtual Product Product { get; set; }
    }
}
