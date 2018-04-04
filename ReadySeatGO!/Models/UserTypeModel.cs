using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class UserTypeModel
    {
        [Key]
        public int UserTypeID { get; set; }

        [Display(Name = "User Type")]
        public string Desc { get; set; }

        public int TotalCount { get; set; }
    }
}