using ReadySeatGO_.App_Code;
using ReadySeatGO_.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadySeatGO_.Controllers
{
    public class RestaurantController : Controller
    {
        public List<SelectListItem> GetCategories()
        {
            var list = new List<SelectListItem>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT RSG_CatID, RSG_Category FROM RSG_Categories
                    ORDER BY RSG_Category";
                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new SelectListItem
                            {
                                Value = Nibutani["RSG_CatID"].ToString(),
                                Text = Nibutani["RSG_Category"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        // Restaurant Application for Patron (User Types)
        // Patrons can apply to have their Restaurant added to the system
        public ActionResult Add()
        {
            RestaurantModel Chuu2 = new RestaurantModel();
            Chuu2.Categories = GetCategories();
            return View(Chuu2);
        }
        [HttpPost]
        public ActionResult Add(RestaurantModel Chuu2, HttpPostedFileBase image)
        {
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"INSERT INTO RSG_Restaurants VALUES
                    (@RSG_UserID, @RSG_CatID, @RSG_ApprovalID, @RSG_RName, @RSG_Address,
                    @RSG_ContactNumber, @RSG_IsFeatured, @RSG_Manager,
                    @RSG_Branch, @RSG_OperatingHours, @RSG_Image, @RSG_Status, @RSG_TotalSeats, 
                    @RSG_DateAdded, @RSG_DateModified)";

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RSG_UserID", Session["userid"].ToString());
                    WickedEye.Parameters.AddWithValue("@RSG_CatID", Chuu2.CatID);
                    WickedEye.Parameters.AddWithValue("@RSG_ApprovalID", 1);

                    WickedEye.Parameters.AddWithValue("@RSG_RName", Chuu2.Restaurant);
                    WickedEye.Parameters.AddWithValue("@RSG_Address", Chuu2.Address);
                    WickedEye.Parameters.AddWithValue("@RSG_ContactNumber", Chuu2.Phone);


                    WickedEye.Parameters.AddWithValue("@RSG_IsFeatured", "No");
                    WickedEye.Parameters.AddWithValue("@RSG_Manager", Chuu2.Manager);
                    WickedEye.Parameters.AddWithValue("@RSG_Branch", Chuu2.Branch);
                    WickedEye.Parameters.AddWithValue("@RSG_OperatingHours", Chuu2.OperatingHours);

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss-") +
                        image.FileName;
                    WickedEye.Parameters.AddWithValue("@RSG_Image", fileName);


                    // Upload the chosen file to images > products
                    image.SaveAs(Server.MapPath("~/Images/Restaurants/" + fileName));

                    WickedEye.Parameters.AddWithValue("@RSG_TotalSeats", Chuu2.TotalSeats);
                    WickedEye.Parameters.AddWithValue("@RSG_Status", "Nothing");
                    WickedEye.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                    WickedEye.Parameters.AddWithValue("@RSG_DateModified", DBNull.Value);
                    WickedEye.ExecuteNonQuery();

                    // Redirect to index
                    return RedirectToAction("Index");

                }
            }



        }


        //public List<RatingsModel> GetRatings()
        //{
        //    var list = new List<RatingsModel>();
        //    using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
        //    {
        //        Rikka.Open();
        //        string Takanashi = @"SELECT RSG_RatingID, RSG_UserID, RSG_Cleanliness, RSG_CustomerService.
        //                             RSG_FoodQuality, RSG_Remarks, RSG_DateAdded FROM RSG_Categories 
        //                             WHERE RSG_RestaurantID=@RID ORDER BY RSG_DateAdded";
        //        using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
        //        {
        //            using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
        //            {
        //                while (Nibutani.Read())
        //                {
        //                    list.Add(new RatingsModel
        //                    {
        //                        RatingID = int.Parse(Nibutani["RSG_RatingID"].ToString()),
        //                        restaurantID = int.Parse(Nibutani["RSG"].ToString()),
        //                        userID = int.Parse(Nibutani["RSG_UserID"].ToString()),
        //                        Cleanliness = int.Parse(Nibutani["RSG_Cleanliness"].ToString()),
        //                        CustomerService = int.Parse(Nibutani["RSG_CustomerService"].ToString()),
        //                        FoodQuality = int.Parse(Nibutani["RSG_FoodQuality"].ToString()),
        //                        Remarks = Nibutani["RSG_Remarks"].ToString(),
        //                        DateAdded = DateTime.Parse(Nibutani["RSG_DateAdded"].ToString())




        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return list;
        //}
    }


}