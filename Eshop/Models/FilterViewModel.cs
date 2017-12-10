using Eshop.Data.Entities;
using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class FilterViewModel
    {
        public int? CategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string SearchString { get; set; }
        public IList<Data.Entities.Attribute> Attributes { get; set; }
        public IList<TraitList> FilterAttributes { get; set; }
        public List<Product> Products { get; set; }
        public static explicit operator FilterViewModel(FilterModel m)
        {
            return new FilterViewModel
            {
                CategoryId = m.CategoryId,
                MinPrice = m.MinPrice,
                MaxPrice = m.MaxPrice,
                Attributes = m.Attributes,
                Products = m.Products,
                FilterAttributes = m.FilterAttributes,
                SearchString=m.SearchString
            };
        }
        public static explicit operator FilterModel(FilterViewModel m)
        {
            return new FilterModel
            {
                CategoryId = m.CategoryId,
                MinPrice = m.MinPrice,
                MaxPrice = m.MaxPrice,
                Attributes = m.Attributes,
                Products = m.Products,
                FilterAttributes = m.FilterAttributes,
                SearchString = m.SearchString
            };
        }
    }
}