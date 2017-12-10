using Eshop.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Eshop.Data.Models;
using Eshop.Data.Entities;

namespace Eshop.BusinessLogic.Implementations
{
    public class AdminService : IAdminService
    {
        public IRepository Repository { get; set; }

        public AdminService(IRepository repo)
        {
            Repository = repo;
        }

        public List<Account> GetClientAccountList()
        {
            return Repository.GetAccountList().Where(x => x.Role == Account.AccRole.client ).ToList();

        }

        public List<Account> GetEmployeeAccountList()
        {
            return Repository.GetAccountList().Where(x => x.Role == Account.AccRole.employee).ToList();
        }
        
        
    }
}
