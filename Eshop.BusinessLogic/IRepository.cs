using Eshop.Data.Entities;
using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eshop.BusinessLogic
{
    public interface IRepository
    {
        JsonResult GetCategories();
        void InsertCategory();
        void InsertCategory(CategoryModel model);
        void Register(AccountModel a);
        Account GetAccountByEmail(string email);
        string InsertAccount(Account a);
    }
}
