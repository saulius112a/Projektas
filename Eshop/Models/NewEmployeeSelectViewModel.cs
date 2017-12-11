using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Eshop.Models
{
    public class NewEmployeeSelectViewModel
    {
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        
        public IEnumerable<SelectListItem> Options { get; set; }
    }
}