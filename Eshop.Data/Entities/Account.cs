﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Account:BaseEntity
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
        public virtual AccountInfo AccountInfo { get; set; }
    }
}
