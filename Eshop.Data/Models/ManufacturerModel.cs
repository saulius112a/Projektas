using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebLink { get; set; }
        public string IconLocation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public static explicit operator ManufacturerModel(Manufacturer m)
        {
            return new ManufacturerModel
            {
                Name = m.Name,
                Description = m.Description,
                WebLink = m.WebLink,
                IconLocation = m.IconLocation,
                Products = m.Products,
                Id=m.Id
            };
        }
        public static explicit operator Manufacturer(ManufacturerModel m)
        {
            return new Manufacturer
            {
                Name = m.Name,
                Description = m.Description,
                WebLink = m.WebLink,
                IconLocation = m.IconLocation,
                Products = m.Products,
                Id = m.Id
            };
        }
    }
}
