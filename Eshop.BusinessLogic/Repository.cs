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
    }
}
