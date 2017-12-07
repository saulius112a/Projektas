using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Eshop.Data.Entities.LoginLog;

namespace Eshop.Data.Models
{
    public class LoginLogsModel
    {
        public string IPAddress { get; set; }
        public LogStatus Status { get; set; }
        public int AccountId { get; set; }

        public static explicit operator LoginLogsModel(LoginLog l)
        {
            return new LoginLogsModel
            {
                IPAddress = l.IPAddress,
                Status = l.Status,
                AccountId = l.AccountId
            };
        }

        public static explicit operator LoginLog(LoginLogsModel l)
        {
            return new LoginLog
            {
                IPAddress = l.IPAddress,
                Status = l.Status,
                AccountId = l.AccountId
            };
        }

    }
}
