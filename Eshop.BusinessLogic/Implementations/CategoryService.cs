using Eshop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Data.Entities;
using Eshop.Data.Models;

namespace Eshop.BusinessLogic.Implementations
{
    public class CategoryService : ICategoryService
    {
        IRepository repo { get; set; }
        public CategoryService (IRepository repo)
        {
            this.repo = repo;
        }

        public void InsertCategory(CategoryModel model)
        {
            repo.InsertCategory(model);
        }
    }
}
