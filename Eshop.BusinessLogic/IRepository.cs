using Eshop.Data.Entities;
using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eshop.BusinessLogic
{
    public interface IRepository
    {
        void InsertCategory();
        void InsertCategory(CategoryModel model);
        void Register(AccountModel a);
        Account GetAccountByEmail(string email);
        string InsertAccount(Account a);
        AccountInfo GetAccountInfo(int id);
        string InsertAccountInfo(int id, AccountInfo a);
        string UpdateAccountInfo(int id, AccountInfo a);
        string InsertLoginLog(LoginLog l);
        List<CategoryModel> GetParentCategories();
        void InsertCategory(Category category);
        void InsertAttributes(Category category);
        void InsertManufacturer(Manufacturer manufacturer);
        void InsertManufacturersFromJsonFile(StreamReader reader);
        void InsertCategoriesFromJsonFile(StreamReader reader);
        void InsertCategoryAttributesFromJsonFile(StreamReader reader);
        void InsertProductsFromJsonFile(StreamReader reader);
        List<Manufacturer> GetManufacturers();
        Category GetCategory(int id);
        List<Account> GetAccountList();
    }
}
