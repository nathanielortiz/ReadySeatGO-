using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class CategoriesModel
    {
        [Key]
        public int CatID { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }

        public int TotalCount { get; set; }
    }
}