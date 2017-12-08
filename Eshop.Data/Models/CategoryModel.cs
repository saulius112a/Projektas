using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public virtual Category Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual IList<Entities.Attribute> Attributes { get; set; }
        public static explicit operator CategoryModel(Category c)
        {
            return new CategoryModel
            {
                Name = c.Name,
                Description=c.Description,
                Id=c.Id,
                ParentId=c.ParentId,
                Parent=c.Parent,
                SubCategories=c.SubCategories,
                Attributes=c.Attributes
            };
        }
        public static explicit operator Category(CategoryModel c)
        {
            return new Category
            {
                Name = c.Name,
                Description=c.Description,
                Id=c.Id,
                ParentId=c.ParentId,
                Parent = c.Parent,
                SubCategories = c.SubCategories,
                Attributes = c.Attributes
            };
        }
    }
}
