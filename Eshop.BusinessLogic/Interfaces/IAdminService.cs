using Eshop.Data.Entities;
using System;
using System.Collections.Generic;

namespace Eshop.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        List<Account> GetClientAccountList();
        List<Account> GetEmployeeAccountList();
        List<Account> GetAccountList(DateTime? start, DateTime? end);
    }
}
