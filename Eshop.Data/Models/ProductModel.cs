using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string ProductCoode { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDiscounted { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual IList<ProductAttribute> Attributes { get; set; }
        public virtual ICollection<ProductPic> Pics { get; set; }
        public virtual Discount Discount { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public static explicit operator ProductModel (Product p)
        {
            return new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Color = p.Color,
                CreationDate = p.CreationDate,
                UpdateDate = p.UpdateDate,
                IsDiscounted = p.IsDiscounted,
                CategoryId=p.CategoryId,
                Category=p.Category
            };
        }
        public static explicit operator Product(ProductModel p)
        {
            return new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Color = p.Color,
                CreationDate = p.CreationDate,
                UpdateDate = p.UpdateDate,
                IsDiscounted = p.IsDiscounted,
                CategoryId = p.CategoryId,
                Category = p.Category
            };
        }
    }
}
