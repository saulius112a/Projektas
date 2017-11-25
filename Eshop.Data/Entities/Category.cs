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
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

    }
}
