using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Data.Entities;
using Eshop.Data.DatabaseContext;
using Eshop.Data.Models;
using System.Web.Mvc;

namespace Eshop.BusinessLogic
{
    public class Repository : IRepository
    {
        public MySqlDbContext db { get; private set; }
        public Repository(MySqlDbContext db)
        {
            this.db = db;
        }
        public JsonResult GetCategories()
        {
            var rez= db.Categories.Where(c => c.SubCategories.ToList().Count > 0).Select(x=>new
            {
                x.Name,
                x.ParentId,
                subCat = x.SubCategories.Select(y =>new
                {
                    y.Name,
                    y.ParentId
                })
            }).ToList();

            return new JsonResult() { Data = rez, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public void InsertCategory()
        {
            db.SaveChangesAsync();
        }
        public void InsertCategory(CategoryModel model)
        {
            //Category parent=db.
            db.Categories.Add((Category)model);
            db.SaveChangesAsync();
        }

        public void Register(AccountModel a)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByEmail(string email)
        {
            return db.Accounts.FirstOrDefault(a => a.Email == email);
        }
        public string InsertAccount(Account a)
        {
            try
            {
                db.Accounts.Add(a);
                db.SaveChanges();
                return "";
            } catch (Exception e)
            {
                return e.Message;
            }
        }
        public AccountInfo GetAccountInfo(int id)
        {
            return db.AccountInfos.FirstOrDefault(a => a.AccountId == id);
        }

        public string InsertAccountInfo(int id, AccountInfo a)
        {
            try
            {
                a.AccountId = id;
                db.AccountInfos.Add(a);
                db.SaveChanges();
                return "";
            } catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateAccountInfo(int id, AccountInfo a)
        {
            try
            {
                AccountInfo ai = db.AccountInfos.FirstOrDefault(b => b.AccountId == id);
                ai.Name = a.Name;
                ai.LastName = a.LastName;
                ai.Address = a.Address;
                ai.Phone = a.Phone;
                ai.ZipCode = a.ZipCode;
                db.SaveChanges();
                return "";
            } catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
