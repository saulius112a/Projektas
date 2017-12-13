using Eshop.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Eshop.Data.Models;
using Eshop.Data.Entities;
using System;

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
            return Repository.GetAccountList().Where(x => x.Role == Account.AccRole.client && x.Status != Account.AccStatus.deleted).ToList();

        }

        public List<Account> GetEmployeeAccountList()
        {
            return Repository.GetAccountList().Where(x => x.Role == Account.AccRole.employee && x.Status != Account.AccStatus.deleted).ToList();
        }

        public List<Account> GetAccountList(DateTime? start, DateTime? end)
        {
            var list = Repository.GetAccountList()
                .Where(x => start == null ? true : Nullable.Compare(x.CreationDate, start) > 0)
                .Where(x => end == null ? true : Nullable.Compare(x.CreationDate, end) < 0)
                .OrderBy(x => x.AccountInfo.LastName)
                .ThenBy(x => x.AccountInfo.Name)
                .ToList();

            foreach (var item in list)
            {
                item.LoginLogs = item.LoginLogs.OrderByDescending(x => x.LoginDate).ToList();
            }
            return list;
        }


    }
}
