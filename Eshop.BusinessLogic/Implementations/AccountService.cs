using Eshop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Data.Models;
using System.Security.Claims;
using Eshop.Data.Entities;

namespace Eshop.BusinessLogic.Implementations
{
    public class AccountService : IAccountService
    {
        public IRepository Repository { get; set; }
        public AccountService(IRepository repo)
        {
            Repository = repo;
        }
        public ClaimsIdentity Authorise(AccountModel a)
        {
            throw new NotImplementedException();
        }

        public string Register(AccountModel a)
        {
            Account dbAccount = Repository.GetAccountByEmail(a.Email);
            if(dbAccount == null)
            {
                var dbRsp = Repository.InsertAccount((Account)a);
                return (String.IsNullOrWhiteSpace(dbRsp) ? null : ("Problems with database insert action: " + dbRsp));
            } else if (dbAccount != null)
            {
                return "1";
            }
            return "2";
        }
        public AccountInfoModel GetAccountInfo(int id)
        {
            return (AccountInfoModel)Repository.GetAccountInfo(id);
        }

        public AccountModel GetAccountByEmail(string email)
        {
            return (AccountModel)Repository.GetAccountByEmail(email);
        }
    }
}
