using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Data.Entities;
using Eshop.Data.DatabaseContext;
using Eshop.Data.Models;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;

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

        public string InsertLoginLog(LoginLog l)
        {
            try
            {
                db.LoginLogs.Add(l);
                db.SaveChanges();
                return "";
            } catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<CategoryModel> GetParentCategories()
        {
            var list = db.Categories.Where(x => x.ParentId == null).ToList();
            List<CategoryModel> res = list.Select(x => (CategoryModel)x).ToList();
            res.Insert(0, new CategoryModel
            {
                Name = "-"
            });
            return res;
        }

        public void InsertCategory(Category category)
        {
            if (category.ParentId == 0)
            {
                category.ParentId = null;
            }
            db.Categories.Add(category);
            db.SaveChangesAsync();
        }

        public void InsertAttributes(Category category)
        {
            if (category.Attributes == null || category.Attributes.Count <= 0)
            {
                return;
            }
            Category cat = db.Categories.Where(x => x.Id == category.Id).First();
            db.Attributes.RemoveRange(cat.Attributes);
            cat.Attributes = category.Attributes;
            db.SaveChangesAsync();
        }

        public void InsertManufacturer(Manufacturer manufacturer)
        {
            db.Manufacturers.Add(manufacturer);
            db.SaveChangesAsync();
        }

        public void InsertManufacturersFromJsonFile(StreamReader reader)
        {
            string json = reader.ReadToEnd();
            var res = JObject.Parse(json);
            var v = res.Property("manufacturers");
            //var arr = v.Value;
            JArray arr = JArray.Parse(v.Value.ToString());
            List<Manufacturer> list = new List<Manufacturer>();
            db.Configuration.AutoDetectChangesEnabled = false;
            for (int i = 0; i < arr.Count; i++)
            {
                string name = arr[i].Value<string>("name");
                string webLink = arr[i].Value<string>("webLink");
                string description = arr[i].Value<string>("description");
                string iconLink = arr[i].Value<string>("iconLink");
                Manufacturer manu=db.Manufacturers.Where(x => x.Name == name).FirstOrDefault();
                if (manu != null)
                {
                    manu.Name = name;
                    manu.WebLink = webLink;
                    manu.Description = description;
                    manu.IconLocation = iconLink;
                }
                else
                {
                    db.Manufacturers.Add(new Manufacturer
                    {
                        Name = name,
                        WebLink = webLink,
                        Description = description,
                        IconLocation = iconLink
                    });
                }
            }
            db.ChangeTracker.DetectChanges();         
            db.SaveChangesAsync();
        }
        public void InsertCategoriesFromJsonFile(StreamReader reader)
        {
            string json = reader.ReadToEnd();
            var res = JObject.Parse(json);
            var v = res.Property("categories");
            //var arr = v.Value;
            JArray arr = JArray.Parse(v.Value.ToString());
            db.Configuration.AutoDetectChangesEnabled = false;
            for (int i = 0; i < arr.Count; i++)
            {
                string name = arr[i].Value<string>("name");
                string parent = arr[i].Value<string>("parent");
                string description = arr[i].Value<string>("description");
                Category parentEnt=db.Categories.Where(x => x.Name == parent).FirstOrDefault();
                Category cat = db.Categories.Where(x => x.Name == name).FirstOrDefault();
                if (cat != null)
                {
                    cat.Name = name;
                    cat.Description = description;
                    cat.Parent = parentEnt;
                    if (parentEnt != null)
                    {
                        cat.ParentId = parentEnt.Id;
                    }
                    
                }
                else
                {
                    Category newCat = new Category
                    {
                        Name = name,
                        Description = description,
                        Parent = parentEnt

                    };
                    newCat.ParentId = parentEnt.Id;
                db.Categories.Add(newCat);
                }
            }
            db.ChangeTracker.DetectChanges();
            db.SaveChangesAsync();
        }
        public void InsertCategoryAttributesFromJsonFile(StreamReader reader)
        {
            string json = reader.ReadToEnd();
            var res = JObject.Parse(json);
            var v = res.Property("attributes");
            //var arr = v.Value;
            JArray arr = JArray.Parse(v.Value.ToString());
            db.Configuration.AutoDetectChangesEnabled = false;
            for (int i = 0; i < arr.Count; i++)
            {
                string categoryName = arr[i].Value<string>("categoryName");
                Category cat = db.Categories.Where(x => x.Name == categoryName).FirstOrDefault();
                if (cat == null)
                {
                    continue;
                }
                JArray attributes = arr[i].Value<JArray>("categoryAttributes");
                for(int j = 0; j < attributes.Count; j++)
                {
                    string attributeName = attributes[j].Value<string>("name");
                    bool isTrait= attributes[j].Value<bool>("isTrait");
                    string description= attributes[j].Value<string>("description");
                    Data.Entities.Attribute att =cat.Attributes.Where(x => x.Name == attributeName).FirstOrDefault();
                    if (att != null)
                    {
                        att.Name = attributeName;
                        att.IsTrait = isTrait;
                        att.Description = description;
                    }
                    else
                    {
                        db.Attributes.Add(new Data.Entities.Attribute
                        {
                            Name = attributeName,
                            IsTrait = isTrait,
                            Description = description,
                            Category = cat,
                            CategoryId = cat.Id
                        });
                    }

                }

            }
            db.ChangeTracker.DetectChanges();
            db.SaveChangesAsync();
        }

        public List<Manufacturer> GetManufacturers()
        {
            return db.Manufacturers.ToList();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
