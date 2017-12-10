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
                Manufacturer manu = db.Manufacturers.Where(x => x.Name == name).FirstOrDefault();
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
            JArray arr = JArray.Parse(v.Value.ToString());
            db.Configuration.AutoDetectChangesEnabled = false;
            for (int i = 0; i < arr.Count; i++)
            {
                string name = arr[i].Value<string>("name");
                string parent = arr[i].Value<string>("parent");
                string description = arr[i].Value<string>("description");
                Category parentEnt = db.Categories.Where(x => x.Name == parent).FirstOrDefault();
                Category cat = db.Categories.Where(x => x.Name == name).FirstOrDefault();
                if (cat != null)
                {
                    cat.Name = name;
                    cat.Description = description;
                    if (parentEnt != null)
                    {
                        cat.ParentId = parentEnt.Id;
                        cat.Parent = parentEnt;
                    }
                }
                else
                {
                    Category newCat = new Category
                    {
                        Name = name,
                        Description = description
                    };

                    if (parentEnt != null)
                    {
                        newCat.ParentId = parentEnt.Id;
                        newCat.Parent = parentEnt;
                    }
                    if (parent == null)
                    {
                        db.ChangeTracker.DetectChanges();
                        db.Categories.Add(newCat);
                        db.SaveChangesAsync();
                        db.Configuration.AutoDetectChangesEnabled = false;
                    }
                    else
                    {
                        db.Categories.Add(newCat);
                    }
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
                for (int j = 0; j < attributes.Count; j++)
                {
                    string attributeName = attributes[j].Value<string>("name");
                    bool isTrait = attributes[j].Value<bool>("isTrait");
                    string description = attributes[j].Value<string>("description");
                    Data.Entities.Attribute att = cat.Attributes.Where(x => x.Name == attributeName).FirstOrDefault();
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
        public void InsertProductsFromJsonFile(StreamReader reader)
        {
            string json = reader.ReadToEnd();
            var res = JObject.Parse(json);
            var v = res.Property("products");
            JArray arr = JArray.Parse(v.Value.ToString());
            db.Configuration.AutoDetectChangesEnabled = false;
            for (int i = 0; i < arr.Count; i++)
            {
                string name = arr[i].Value<string>("name");
                string manufacturer = arr[i].Value<string>("manufacturer");
                string category = arr[i].Value<string>("category");
                Manufacturer man = db.Manufacturers.Where(x => x.Name == manufacturer).FirstOrDefault();
                Category cat = db.Categories.Where(x => x.Name == category).FirstOrDefault();
                if (man == null || cat == null)
                {
                    continue;
                }
                string productCode = arr[i].Value<string>("productCode");
                string color = arr[i].Value<string>("color");
                string description = arr[i].Value<string>("description");
                double weight = arr[i].Value<double>("weight");
                double price = arr[i].Value<double>("price");
                bool isDiscounted = arr[i].Value<bool>("isDiscounted");
                DateTime date = DateTime.Now;
                Product product = db.Products.Where(x => x.Name == name).FirstOrDefault();
                if (product != null)
                {
                    product.Name = name;
                    product.ManufacturerId = man.Id;
                    product.Manufacturer = man;
                    product.Price = price;
                    product.ProductCode = productCode;
                    product.UpdateDate = date;
                    product.Color = color;
                    product.Weight = weight;
                    product.Description = description;
                    product.Category = cat;
                    product.CategoryId = cat.Id;
                    product.IsDiscounted = isDiscounted;
                }
                else
                {
                    product = new Product
                    {
                        Name = name,
                        Description = description,
                        ProductCode = productCode,
                        Weight = weight,
                        Price = price,
                        Color = color,
                        UpdateDate = date,
                        CreationDate = date,
                        IsDiscounted = isDiscounted,
                        Manufacturer = man,
                        ManufacturerId = man.Id,
                        Category = cat,
                        CategoryId = cat.Id

                    };
                    db.Products.Add(product);
                }
                JArray attributes = arr[i].Value<JArray>("attributes");
                for (int j = 0; j < attributes.Count; j++)
                {
                    string attributeName = attributes[j].Value<string>("name");
                    Data.Entities.Attribute att = db.Attributes.Where(x => x.Name == attributeName).FirstOrDefault();
                    if (att != null)
                    {

                        string unit = attributes[j].Value<string>("unit");
                        ProductAttribute productAtt = db.ProductAttributes.Where(
                            x => x.ProductId == product.Id && x.AttributeId == att.Id).FirstOrDefault();
                        if (productAtt != null)
                        {
                            if (att.IsTrait)
                            {
                                string value = attributes[j].Value<string>("value");
                                TraitValue trait = db.TraitValues.Where(x => x.ProductAttributeId == productAtt.Id).FirstOrDefault();
                                if (trait != null)
                                {
                                    trait.Value = value;
                                }
                                else
                                {
                                    db.TraitValues.Add(new TraitValue
                                    {
                                        Value = value,
                                        ProductAttributeId = productAtt.Id,
                                        ProductAttribute = productAtt
                                    });
                                }
                            }
                            else
                            {
                                double value = attributes[j].Value<double>("value");
                                Measurement trait = db.Measurements.Where(x => x.ProductAttributeId == productAtt.Id).FirstOrDefault();
                                if (trait != null)
                                {
                                    trait.Value = value;
                                    trait.Unit = unit;
                                }
                                else
                                {
                                    db.Measurements.Add(new Measurement
                                    {
                                        Value = value,
                                        Unit = unit,
                                        ProductAttributeId = productAtt.Id,
                                        ProductAttribute = productAtt
                                    });
                                }
                            }
                        }
                        else
                        {
                            productAtt = new ProductAttribute
                            {
                                Product = product,
                                Attribute = att,
                                AttributeId = att.Id,
                                ProductId = product.Id
                            };
                            db.ProductAttributes.Add(productAtt);
                            if (att.IsTrait)
                            {
                                string value = attributes[j].Value<string>("value");
                                db.TraitValues.Add(new TraitValue
                                {
                                    Value = value,
                                    ProductAttributeId = productAtt.Id,
                                    ProductAttribute = productAtt
                                });
                            }
                            else
                            {
                                double value = attributes[j].Value<double>("value");
                                db.Measurements.Add(new Measurement
                                {
                                    Value = value,
                                    Unit = unit,
                                    ProductAttributeId = productAtt.Id,
                                    ProductAttribute = productAtt
                                });
                            }
                        }
                    }
                }
            }
            db.ChangeTracker.DetectChanges();
            db.SaveChangesAsync();
        }
        public List<Manufacturer> GetManufacturers(string searchString = null)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return db.Manufacturers.Where(x => x.Name.Contains(searchString)).ToList();
            }
            return db.Manufacturers.ToList();
        }
        public List<Category> GetCategories(string searchString, bool getParent, bool onlyParents)
        {
            if (onlyParents)
            {
                return db.Categories.Where(x => x.ParentId == null).ToList();
            }
            IQueryable<Category> DBset = db.Categories;
            if (!getParent)
            {
                DBset = DBset.Where(x => x.ParentId != null);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                return DBset.Where(x => x.Name.Contains(searchString)).ToList();
            }
            return DBset.ToList();
        }
        public List<Product> GetProducts(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return db.Products.Where(x => x.Name.Contains(searchString) || x.ProductCode.Contains(searchString)).ToList();
            }
            return db.Products.ToList();
        }
        public Product GetProduct(int id)
        {
            return db.Products.Where(x => x.Id == id).FirstOrDefault();
        }
        public Category GetCategory(int id)
        {
            return db.Categories.Where(x => x.Id == id).FirstOrDefault();
        }
        public Manufacturer GetManufacturer(int id)
        {
            return db.Manufacturers.Where(x => x.Id == id).FirstOrDefault();
        }
        public void EditManufacturer(Manufacturer manufacturer)
        {
            var oldManufacturer = db.Manufacturers.Where(x => x.Id == manufacturer.Id).FirstOrDefault();
            if (oldManufacturer != null)
            {
                oldManufacturer.Name = manufacturer.Name;
                oldManufacturer.Description = manufacturer.Description;
                oldManufacturer.WebLink = manufacturer.WebLink;
            }
            db.SaveChangesAsync();
        }
        public void EditProduct(Product product)
        {
            var oldProduct = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Weight = product.Weight;
                oldProduct.Description = product.Description;
                oldProduct.ProductCode = product.ProductCode;
                oldProduct.Price = product.Price;
                oldProduct.Color = product.Color;
                oldProduct.UpdateDate = DateTime.Now;
                oldProduct.IsDiscounted = product.IsDiscounted;
                oldProduct.ManufacturerId = product.ManufacturerId;
                for (int i = 0; i < product.Attributes.Count; i++)
                {
                    if (oldProduct.Category.Attributes[i].IsTrait)
                    {
                        oldProduct.Attributes[i].TraitValue.Value = product.Attributes[i].TraitValue.Value;
                    }
                    else
                    {
                        oldProduct.Attributes[i].Measurement.Value = product.Attributes[i].Measurement.Value;
                        oldProduct.Attributes[i].Measurement.Unit = product.Attributes[i].Measurement.Unit;

                    }
                }
            }
            db.SaveChangesAsync();
        }
        public void InsertProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChangesAsync();
        }
        public void EditCategory(Category cat)
        {
            var oldCategory = db.Categories.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (oldCategory != null)
            {
                oldCategory.Name = cat.Name;
                oldCategory.Description = cat.Description;
                oldCategory.ParentId = cat.ParentId;
                oldCategory.Parent = cat.Parent;
            }
            db.SaveChangesAsync();
        }

        public List<Data.Entities.Attribute> GetAttributes(int categoryId)
        {
            return db.Attributes.Where(x => x.CategoryId == categoryId).ToList();
        }
        public List<ProductAttribute> GetProducAttributes(List<Data.Entities.Attribute> attributes)
        {
            List<ProductAttribute> list = new List<ProductAttribute>();
            foreach (Data.Entities.Attribute att in attributes)
            {
                ProductAttribute attribute = db.ProductAttributes.Where(x => x.AttributeId == att.Id).FirstOrDefault();
                if (attribute != null)
                {
                    // list.Add
                }
            }
            return list;
        }
        public List<List<TraitModel>> GetTraitValues(List<Data.Entities.Attribute> attributes)
        {
            List<List<TraitModel>> list = new List<List<TraitModel>>();
            foreach (Data.Entities.Attribute att in attributes)
            {
                if (att.IsTrait)
                {
                    List<TraitModel> temp = new List<TraitModel>();
                    var dbTemp = db.ProductAttributes.Where(x => x.AttributeId == att.Id).Select(x => x.TraitValue)
                        .GroupBy(x => x.Value).ToList();
                    foreach(var trait in dbTemp)
                    {
                        temp.Add(new TraitModel
                        {
                            Name = att.Name,
                            StringValue = trait.Key
                        });
                    }
                    list.Add(temp);
                }
                else
                {
                    List<TraitModel> temp = new List<TraitModel>();
                    list.Add(temp);
                }
            }
            return list;
        }
        public List<TraitList> Temp(List<Data.Entities.Attribute> attributes)
        {
            List<TraitList> temp = new List<TraitList>();
            foreach (Data.Entities.Attribute att in attributes)
            {
                if (att.IsTrait)
                {
                    List<TraitModel> innerList = new List<TraitModel>();
                    var dbTemp = db.ProductAttributes.Where(x => x.AttributeId == att.Id).Select(x => x.TraitValue)
                        .GroupBy(x => x.Value).ToList();
                    foreach (var trait in dbTemp)
                    {
                        innerList.Add(new TraitModel
                        {
                            Name = att.Name,
                            StringValue = trait.Key
                        });
                    }
                    temp.Add(new TraitList { List = innerList });
                }
                else
                {
                    List<TraitModel> innerList = new List<TraitModel>();
                    var dbTemp = db.ProductAttributes.Where(x => x.AttributeId == att.Id).Select(x => x.Measurement)
                        .GroupBy(x => x.Value).ToList();
                    foreach (var trait in dbTemp)
                    {
                        innerList.Add(new TraitModel
                        {
                            Name = att.Name,
                            Value = trait.Key
                        });
                    }
                    temp.Add(new TraitList { List = innerList });
                }
            }
            return temp;
        }
        public List<Product> GetProducts(int categoryId)
        {
            return db.Products.Where(x => x.CategoryId == categoryId).ToList();
        }
        public List<Product> GetProducts(FilterModel model)
        {
            IQueryable<ProductAttribute> DBset2 = db.ProductAttributes.Where(x => x.Product.CategoryId == model.CategoryId);
            if (!String.IsNullOrEmpty(model.SearchString))
            {
                DBset2 = DBset2.Where(x => x.Product.Name.ToLower().Contains(model.SearchString.ToLower()) || x.Product.ProductCode.ToLower().Contains(model.SearchString.ToLower()));
            }
            if (model.MinPrice != null)
            {
                DBset2 = DBset2.Where(x => x.Product.Price >= model.MinPrice);
            }
            if (model.MaxPrice != null)
            {
                DBset2 = DBset2.Where(x => x.Product.Price <= model.MaxPrice);
            }
            List<Product> listProducts = new List<Product>();
            bool alreadyAdded = false;
            bool noCheckedValues = true;
            for(int i = 0; i < model.Attributes.Count; i++)
            {
                var att = model.Attributes[i];
                if (!alreadyAdded)
                {
                    if (att.IsTrait)
                    {
                        for (int j = 0; j < model.FilterAttributes[i].List.Count; j++)
                        {
                            var filter = model.FilterAttributes[i].List[j];
                            if (filter.Checked)
                            {
                                noCheckedValues = false;
                                var something = DBset2.Where(x => x.AttributeId == att.Id && x.TraitValue.Value.ToLower() == filter.StringValue.ToLower()).Select(x => x.Product);
                                listProducts.AddRange(something);

                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < model.FilterAttributes[i].List.Count; j++)
                        {
                            var filter = model.FilterAttributes[i].List[j];
                            if (filter.Checked)
                            {
                                noCheckedValues = false;
                                var something = DBset2.Where(x => x.AttributeId == att.Id && x.Measurement.Value == filter.Value).Select(x => x.Product);
                                listProducts.AddRange(something);
                            }
                        }
                    }
                    if (listProducts.Count > 0)
                    {
                        alreadyAdded = true;
                    }
                }
                else
                {
                    if (att.IsTrait)
                    {
                        for(int h = 0; h < listProducts.Count; h++)
                        {
                            bool checkedForValues = false;
                            bool remove = true;
                            for (int j = 0; j < model.FilterAttributes[i].List.Count; j++)
                            {
                                var filter = model.FilterAttributes[i].List[j];
                                if (filter.Checked)
                                {
                                    noCheckedValues = false;
                                    checkedForValues = true;
                                    if (listProducts[h].Attributes[i].TraitValue.Value.ToLower() == filter.StringValue.ToLower())
                                    {
                                        remove = false;
                                    }
                                }
                            }
                            if (remove&& checkedForValues)
                            {
                                listProducts.RemoveAt(h);
                            }
                        }
                    }
                    else
                    {
                        for (int h = 0; h < listProducts.Count; h++)
                        {
                            bool checkedForValues = false;
                            bool remove = true;
                            for (int j = 0; j < model.FilterAttributes[i].List.Count; j++)
                            {
                                var filter = model.FilterAttributes[i].List[j];
                                if (filter.Checked)
                                {
                                    noCheckedValues = false;
                                    checkedForValues = true;
                                    if (listProducts[h].Attributes[i].Measurement.Value == filter.Value)
                                    {
                                        remove = false;
                                    }
                                }
                            }
                            if (remove&& checkedForValues)
                            {
                                listProducts.RemoveAt(h);
                            }
                        }
                    }
                }     
            }
            if (noCheckedValues && listProducts.Count == 0)
            {
                listProducts = DBset2.GroupBy(x=>x.Product).Select(x=>x.Key).ToList();
            }
            return listProducts;
        }
        public List<MinMax> GetValues(List<Data.Entities.Attribute> attributes)
        {
            List<MinMax> list = new List<MinMax>();
            foreach (Data.Entities.Attribute att in attributes)
            {
                //ProductAttribute attribute = db.ProductAttributes.Where(x => x.AttributeId == att.Id).FirstOrDefault();
                if (!att.IsTrait)
                {
                    var temp = db.ProductAttributes.Where(x => x.AttributeId == att.Id).Select(x=>x.Measurement).OrderBy(x=>x.Value).ToList();
                    double min = temp.FirstOrDefault().Value;
                    double max = temp.LastOrDefault().Value;
                    double average = (max + min) / temp.Count;
                    list.Add(new MinMax
                    {
                        Max = max,
                        Min = min,
                        Average = average
                    });
                }
                else
                {
                    list.Add(new MinMax { });
                }                
            }
            return list;
        }

        public void DeleteManufacturer(int id)
        {
            Manufacturer manu =db.Manufacturers.Where(x => x.Id == id).FirstOrDefault();
            db.Manufacturers.Remove(manu);
            db.SaveChangesAsync();
        }
        public void DeleteCategory(int id)
        {
            Category manu = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            db.Categories.Remove(manu);
            db.SaveChangesAsync();
        }
        public void DeleteProduct(int id)
        {
            Product manu = db.Products.Where(x => x.Id == id).FirstOrDefault();
            IList<ProductAttribute> att = manu.Attributes.ToList();
            db.Configuration.AutoDetectChangesEnabled = false;
            for (int i = 0; i < att.Count; i++)
            {
                var trait = att[i].TraitValue;
                var mesur = att[i].Measurement;
                if (trait != null)
                {
                    db.TraitValues.Remove(trait);
                }
                if (mesur != null)
                {
                    db.Measurements.Remove(mesur);
                }     
            }
            db.ProductAttributes.RemoveRange(att);
            db.Products.Remove(manu);
            db.ChangeTracker.DetectChanges();
            db.SaveChangesAsync();
        }
    }

}
