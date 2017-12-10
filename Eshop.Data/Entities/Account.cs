using System;
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
        [Display(Name = "El. paštas")]
        public string Email { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Rolė")]
        public AccRole Role { get; set; }

        [Required]
        [Display(Name = "Sukūrimo data")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Statusas")]
        public AccStatus Status { get; set; }

        public virtual ICollection<LoginLog> LoginLogs { get; set; }

        public virtual AccountInfo AccountInfo { get; set; }

        public enum AccStatus
        {
            active,banned,deleted
        }

        public enum AccRole
        {
            client,employee,admin
        }
    }
}
