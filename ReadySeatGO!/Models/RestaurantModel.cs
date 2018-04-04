using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadySeatGO_.Models
{
    public class RestaurantModel
    {
        [Key]
        [Display(Name = "Restaurant ID")]
        public int RestaurantID { get; set; }


        [Display(Name = "Category")]
        [Required(ErrorMessage = "Select from list.")]
        public int CatID { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string Category { get; set; }


        [Display(Name = "Owner")]
        public int UserID { get; set; }

        public List<UsersModel> User { get; set; }

        [Display(Name = "Owner")]
        public string UserName { get; set; }

        [Display(Name = "Approval ID")]
        public int ApprovalID { get; set; }

        public List <ApprovalModel> Approval { get; set;}

        [Display(Name = "Approval Status")]
        public string ApprovalStatusID { get; set; }
        

       


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "Invalid input length.")]
        public string Restaurant { get; set; }

        [Display(Name = "Address")]
        [MaxLength(50, ErrorMessage = "Incorrect input.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Featured")]
        public string IsFeatured { get; set; }

        [Display(Name = "Contact #")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        public string Phone { get; set; }

        [Display(Name = "Manager")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(50, ErrorMessage = "Incorrect input.")]
        public string Manager { get; set; }

        [Display(Name = "Branch")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        public string Branch { get; set; }

        [Display(Name = "Operating Hours")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        public string OperatingHours { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public string Image { get; set; }

        [Display (Name = "Total Seats")]
        public string TotalSeats { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
    }
}