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
        List<Manufacturer> GetManufacturers(string searchString=null);
        Category GetCategory(int id);

        List<Category> GetCategories(string searchString,bool getParent=true,bool onlyParents=false);
        void EditCategory(Category cat);
        Manufacturer GetManufacturer(int id);
        void EditManufacturer(Manufacturer manufacturer);
        List<Product> GetProducts(string searchString);
        Product GetProduct(int id);
        void EditProduct(Product product);
        void InsertProduct(Product product);
        List<Data.Entities.Attribute> GetAttributes(int categoryId);
        List<ProductAttribute> GetProducAttributes(List<Data.Entities.Attribute> attribute);
        List<MinMax> GetValues(List<Data.Entities.Attribute> attributes);
        List<List<TraitModel>> GetTraitValues(List<Data.Entities.Attribute> attributes);
        List<TraitList> Temp(List<Data.Entities.Attribute> attributes);
        List<Product> GetProducts(int categoryId);
        List<Product> GetProducts(FilterModel model);
        void DeleteManufacturer(int id);
        void DeleteCategory(int id);
        void DeleteProduct(int id);

        List<Account> GetAccountList();
        void DeleteAccount(int accountId);
        void ChangeAccountRole(int accountId, Account.AccRole newRole);

    }
}
