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

        public List<UsersModel> Authors { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }



        public int TotalCount { get; set; }


        [Display(Name = "Cleanliness")]
        [MaxLength(1, ErrorMessage = "Incorrect input.")]
        [Required]
        public int Cleanliness { get; set; }

        [Display(Name = "Customer Service")]
        [MaxLength(1, ErrorMessage = "Incorrect input.")]
        [Required]
        public int CustomerService { get; set; }

        [Display(Name = "Food Quality")]
        [MaxLength(1, ErrorMessage = "Incorrect input.")]
        [Required]
        public int FoodQuality { get; set; }

        [Display(Name = "Remarks")]
        [MaxLength(100, ErrorMessage = "Incorrect input.")]
        [Required]
        public string Remarks { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }



    }
}