using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Data.Models
{
    public class TraitModel
    {
        public string Name { get; set; }
        public string StringValue { get; set; }
        public double Value { get; set; }
        public bool Checked { get; set; }
    }
}