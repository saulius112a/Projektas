﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class BaseEntity
    {
        [Key, Column(Order = 0)]
        public int Id { get;set;}
    }
}
