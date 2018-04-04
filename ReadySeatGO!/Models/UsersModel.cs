using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class UsersModel
    {
        [Key]
        [Display(Name="User ID")]
        public int UserID { get; set; }

        public int TypeID { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "Invalid input length.")]
        public string Username { get; set; }


        [Display(Name = "User Type")]
        public int UserTypeID { get; set; }

        public List<UserTypeModel> UserTypes { get; set; }

        [Display(Name = "User Type")]
        public string UserType { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(20, ErrorMessage = "Incorrect input.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Incorrect format.")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(50, ErrorMessage = "Incorrect input.")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [MaxLength(50, ErrorMessage = "Incorrect input.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Mobile #")]
        [MaxLength(11, ErrorMessage = "Incorrect input.")]
        [RegularExpression(".{11}", ErrorMessage = "Incorrect format.")]
        public string Mobile { get; set; }


        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }




    }
}