using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class RestaurantViewModel
    {
        public List<RestaurantModel> Restaurants { get; set; }
        public List<CategoriesModel> Categories { get; set; }
        public List<RatingsModel> Ratings { get; set; }

    }
}