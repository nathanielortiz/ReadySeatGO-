using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class CheckInModel
    {
        [Key]
        public int CheckInID { get; set; }

        [Display(Name = "User ID")]
        public int UserID { get; set; }

        public List<UsersModel> Users { get; set; }

        [Display(Name = "User Name")]
        public string User{ get; set; }


        [Display(Name = "Restaurant ID")]
        public int RestaurantID { get; set; }

        public List<RestaurantModel> Restaurants { get; set; }

        [Display(Name = "Restaurant Name")]
        public string Restaurant { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public DateTime DateAdded { get; set; }

    }
}