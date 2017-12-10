using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public static class Extensions
    {
        public static string ToQueryString(this IEnumerable<string> items)
        {
            return items.Aggregate("", (curr, next) => curr + "mystring=" + next + "&");
        }
        public static string ToQueryString(this IEnumerable<TraitModel> items)
        {
            return items.Aggregate("", (curr, next) => curr + "mystring=" + next + "&"); ;
        }
    }
}