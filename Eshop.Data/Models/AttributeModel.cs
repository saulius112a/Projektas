using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Models
{
    public class AttributeModel
    {
        public string Name { get; set; }
        public bool IsTrait { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        //public virtual Category Category { get; set; }
        //public virtual ICollection<ProductAttribute>  ProductAttributes { get; set; }
        public static explicit operator AttributeModel(Entities.Attribute attribute)
        {
            return new AttributeModel
            {
                Name=attribute.Name,
                IsTrait=attribute.IsTrait,
                Description=attribute.Description,
                CategoryId=attribute.CategoryId
            };
        }
        public static explicit operator Entities.Attribute(AttributeModel attribute)
        {
            return new Entities.Attribute
            {
                Name = attribute.Name,
                IsTrait = attribute.IsTrait,
                Description = attribute.Description,
                CategoryId = attribute.CategoryId
            };
        }
    }
}
