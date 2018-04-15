using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySeatGO_.Models
{
    public class CheckStatusModel
    {
        public int CheckStatusID { get; set; }
        

        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Branch { get; set; }
        public string OperatingHours { get; set; }
        public string Category { get; set; }
        public string TotalSeats { get; set; }
        public string TotalCheckIns { get; set; }






    }
}