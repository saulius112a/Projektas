using Eshop.Data.Entities;
using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        //List<Category> GetCategories();
        void InsertCategory(CategoryModel model);
        //CategoryModel GetCategory();
    }
}
