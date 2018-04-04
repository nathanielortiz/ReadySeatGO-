using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class ApprovalModel
    {
        [Key]
        public int ApprovalID { get; set; }

        [Display(Name = "Status")]
        public string Name { get; set; }

        public int TotalCount { get; set; }
    }
}