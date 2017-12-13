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

        public string Register(AccountModel a, AccountInfoModel ai)
        {
            Account dbAccount = Repository.GetAccountByEmail(a.Email);
            if(dbAccount == null)
            {
                var dbRsp = Repository.InsertAccount((Account)a);
                var dbRsp2 = Repository.InsertAccountInfo(GetAccountByEmail(a.Email).Id, (AccountInfo)ai);
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

        public string UpdateAccountInfo(int id, AccountInfoModel a)
        {
            var dbRsp = Repository.UpdateAccountInfo(id, (AccountInfo)a);
            return (String.IsNullOrWhiteSpace(dbRsp) ? null : ("Problems with database insert action: " + dbRsp));
        }

        public void LogLogin(bool status, string email, string ip)
        {
            LoginLogsModel llm = new LoginLogsModel();
            llm.IPAddress = ip;
            llm.AccountId = GetAccountByEmail(email).Id;
            switch(status)
            {
                case true:
                    llm.Status = LoginLog.LogStatus.succeded;
                    break;
                case false:
                    llm.Status = LoginLog.LogStatus.failed;
                    break;
            }
            var dbRsp = Repository.InsertLoginLog((LoginLog)llm);
        }

        public void AddFavorites(int id, int accId)
        {
            Repository.AddFavorite(id, accId);
        }
    }
}
