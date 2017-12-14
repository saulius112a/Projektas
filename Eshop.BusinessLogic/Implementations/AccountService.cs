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

        public List<Product> GetFavoriteProducts(int accId)
        {
            List<Product> list = new List<Product>();
            List<WishListProduct> wishlist = Repository.GetFavorites(accId);
            for(int i = 0; i < wishlist.Count; i++)
            {
                Product temp = Repository.GetProduct(wishlist[i].ProductId);
                list.Add(temp);
            }
            return list;
        }

        public void RemoveFavorite(int id, int accId)
        {
            Repository.RemoveFavorite(Repository.GetFavoriteId(accId, id));
        }

        public void AddCart(int id, int accId)
        {
            Repository.AddCart(id, accId);
        }

        public List<Product> GetCartProducts(int accId)
        {
            List<Product> list = new List<Product>();
            List<CartInfo> cartlist = Repository.GetCart(accId);
            for (int i = 0; i < cartlist.Count; i++)
            {
                Product temp = Repository.GetProduct(cartlist[i].ProductId);
                list.Add(temp);
            }
            return list;
        }

        public void RemoveCart(int id, int accId)
        {
            Repository.RemoveCart(Repository.GetCartId(accId, id));
        }

        public int CreatePurchase(int id)
        {
            return Repository.CreatePurchase(id);
        }

        public List<Product> GetPurchaseProducts(int Id)
        {
            List<Product> list = new List<Product>();
            List<PurchaseInfo> purchaselist = Repository.GetPurchase(Id);
            for (int i = 0; i < purchaselist.Count; i++)
            {
                Product temp = Repository.GetProduct(purchaselist[i].ProductId);
                list.Add(temp);
            }
            return list;
        }
    }
}
