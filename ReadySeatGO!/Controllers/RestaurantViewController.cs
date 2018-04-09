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
    public class RestaurantViewController : Controller
    {
        // View Restaurants
        public ActionResult Index()
        {
            var list = new RestaurantViewModel();
            list.Restaurants = GetRestaurants();
            list.Categories = GetCategories();
            return View(list);
        }


        public List<RestaurantModel> GetRestaurants()
        {

            var list = new List<RestaurantModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"";
                if (Request.QueryString["c"] == null)
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, c.RSG_Category FROM RSG_Restaurants r " +
                                "INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID " +
                                "INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID";
                }

                else
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, c.RSG_Category FROM RSG_Restaurants r " +
                                "INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID " +
                                "INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID WHERE r.RSG_CatID=@CatID";

                }

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@CatID", Request.QueryString["c"] == null ? "0" : Request.QueryString["c"].ToString());
                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new RestaurantModel
                            {
                                RestaurantID = int.Parse(Nibutani["RSG_RID"].ToString()),
                                Image = Nibutani["RSG_Image"].ToString(),
                                Restaurant = Nibutani["RSG_RName"].ToString(),
                                UserName = Nibutani["RSG_Username"].ToString(),
                                Category = Nibutani["RSG_Category"].ToString(),
                                OperatingHours = Nibutani["RSG_OperatingHours"].ToString(),

                            });
                        }
                    }
                }


            }

            return list;
        }

        public List<CategoriesModel> GetCategories()
        {
            var list = new List<CategoriesModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT c.RSG_CatID, c.RSG_Category,
                    (SELECT COUNT(r.RSG_RID) FROM RSG_Restaurants r WHERE r.RSG_CatID = c.RSG_CatID) AS TotalCount
                    FROM RSG_Categories c ORDER BY c.RSG_Category";

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    using (SqlDataReader data = WickedEye.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new CategoriesModel
                            {
                                CatID = int.Parse(data["RSG_CatID"].ToString()),
                                Name = data["RSG_Category"].ToString(),
                                TotalCount = int.Parse(data["TotalCount"].ToString())

                            });
                        }
                    }
                    return list;
                }
            }
        }

        // View Restaurant Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");

            }

            var Chuu2 = new RestaurantModel();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT r.RSG_RName, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, c.RSG_CatID FROM RSG_Restaurants r INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID
                               INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID 
                               WHERE r.RSG_RID = @RID";

                //"SELECT r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, c.RSG_Category FROM RSG_Restaurants r " +
                //"INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID " +
                //"INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID"

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RID", id);
                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        if (Nibutani.HasRows)
                        {
                            while (Nibutani.Read())
                            {
                                Chuu2.Restaurant = Nibutani["RSG_RName"].ToString();
                                Chuu2.Image = Nibutani["RSG_Image"].ToString();
                                Chuu2.Restaurant = Nibutani["RSG_RName"].ToString();
                                Chuu2.UserName = Nibutani["RSG_Username"].ToString();
                                Chuu2.OperatingHours = Nibutani["RSG_OperatingHours"].ToString();
                                Chuu2.CatID = int.Parse(Nibutani["RSG_CatID"].ToString());
                            }

                            return View(Chuu2);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }

    }
}