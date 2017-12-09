using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        List<AccountModel> GetClientAccountList();
        List<AccountModel> GetEmployeeAccountList();
    }
}
