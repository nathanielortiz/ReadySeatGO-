using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class RatingsModel
    {
        [Key]
        public int RatingID { get; set; }

        [Display(Name = "Restaurant ID")]
        public int restaurantID { get; set; }

        public List<RestaurantModel> Restaurants { get; set; }

        [Display(Name = "Restaurant")]
        public string Restaurant { get; set; }

        [Display(Name = "User ID")]

        public int userID { get; set; }

        public List<UsersModel> Users { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }




        [Display(Name = "Cleanliness")]
        [Range(1, 5, ErrorMessage = "Input not in range.")]
        [Required]
        public int Cleanliness { get; set; }



        [Display(Name = "Customer Service")]
        [Range(1, 5, ErrorMessage = "Input not in range.")]
        [Required]
        public int CustomerService { get; set; }



        [Display(Name = "Food Quality")]
        [Range(1, 5, ErrorMessage = "Input not in range.")]
        [Required]
        public int FoodQuality { get; set; }




        [Display(Name = "Remarks")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Remarks { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }



    }
}