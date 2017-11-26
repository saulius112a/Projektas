using Eshop.Data.Entities;
using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }

        public static explicit operator CategoryViewModel(CategoryModel c)
        {
            return new CategoryViewModel
            {
                Name = c.Name,
                Description = c.Description,
                Id = c.Id,
                ParentId = c.ParentId,
                Parent=c.Parent,
                SubCategories = c.SubCategories
            };
        }
        public static explicit operator CategoryModel(CategoryViewModel c)
        {
            return new CategoryModel
            {
                Name = c.Name,
                Description = c.Description,
                Id = c.Id,
                ParentId=c.ParentId,
                Parent=c.Parent,
                SubCategories = c.SubCategories,
                
            };
        }
    }
}