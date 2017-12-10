using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Models
{
    public class FilterModel
    {
        public int? CategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string SearchString { get; set; }
        public IList<Data.Entities.Attribute> Attributes { get; set; }
        public IList<TraitList> FilterAttributes { get; set; }
        public List<Product> Products { get; set; }
    }
}
