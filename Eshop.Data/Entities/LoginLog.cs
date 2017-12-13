using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class LoginLog:BaseEntity
    {
        [Required]
        public string IPAddress { get; set; }
        public LogStatus Status { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public DateTime LoginDate { get; set; }

        public enum LogStatus
        {
            failed, succeded
        }
        
    }
}
