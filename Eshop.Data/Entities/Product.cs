using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Entities
{
    public class Product:BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string ProductCoode { get; set; }
        
        [Required]
        public double Price { get; set; }
        public string Color { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDiscounted { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<ProductAttribute> Attributes { get; set; }
        public virtual ICollection<ProductPic> Pics { get; set; }
        public virtual Discount Discount { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<PurchaseInfo> PurchaseInfos { get; set; }
        public virtual ICollection<WishListProduct> WishListProducts { get; set; }
        public virtual ICollection<CartInfo> CartInfos { get; set; }

    }
}
