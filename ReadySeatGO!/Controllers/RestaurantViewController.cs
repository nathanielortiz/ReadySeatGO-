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

        // Check-In for Patron(s)
        public ActionResult CheckIn(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"INSERT INTO RSG_CheckIn VALUES (@RSG_UserID, @RSG_RID, @RSG_Remarks, @RSG_DateAdded)";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_UserID", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.Parameters.AddWithValue("@RSG_Remarks", "Check-in");
                    cmd.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("AdminList");
        }

        // Add to Favorite for Patron(s)
        public ActionResult AddtoFavorite(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"INSERT INTO RSG_Favorites VALUES (@RSG_UserID, @RSG_RID, @RSG_DateAdded)";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_UserID", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    
                    cmd.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("ViewFavorites","RestaurantView");
        }

        public ActionResult ViewFavorites()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var list = new RestaurantViewModel();
                list.Restaurants = GetFavoriteRestaurants();
                list.Categories = GetCategories();
                return View(list);

            }
             
        }

        // View Favorites
        public List<RestaurantModel> GetFavoriteRestaurants()
        {

            var list = new List<RestaurantModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"";
                if (Request.QueryString["c"] == null)
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_RName, c.RSG_Category, r.RSG_OperatingHours, r.RSG_Image FROM RSG_Favorites f INNER JOIN RSG_Restaurants r ON f.RSG_RID = r.RSG_RID " +
                                "INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID " +
                                "INNER JOIN RSG_Users u ON f.RSG_UserID = u.RSG_UserID WHERE f.RSG_UserID = @RSG_UID";
                }

                else
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_RName, c.RSG_Category, r.RSG_OperatingHours, r.RSG_Image FROM RSG_Favorites f INNER JOIN RSG_Restaurants r ON f.RSG_RID = r.RSG_RID " +
                                "INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID " +
                                "INNER JOIN RSG_Users u ON f.RSG_UserID = u.RSG_UserID WHERE f.RSG_UserID = @RSG_UID AND r.RSG_CatID=@CatID";

                }

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@CatID", Request.QueryString["c"] == null ? "0" : Request.QueryString["c"].ToString());
                    WickedEye.Parameters.AddWithValue("@RSG_UID", Session["userid"].ToString());/*Session["userid"].ToString());*/

                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new RestaurantModel
                            {
                                RestaurantID = int.Parse(Nibutani["RSG_RID"].ToString()),
                                Image = Nibutani["RSG_Image"].ToString(),
                                Restaurant = Nibutani["RSG_RName"].ToString(),
                                Category = Nibutani["RSG_Category"].ToString(),
                                OperatingHours = Nibutani["RSG_OperatingHours"].ToString(),

                            });
                        }
                    }
                }


            }

            return list;
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        // Edit Favorites
        public ActionResult RemoveFavorite(int? id)
        {
            if (id == null)
                return RedirectToAction("ViewFavorites");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"DELETE FROM RSG_Favorites 
                    WHERE RSG_RID=@RSG_RID";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("ViewFavorites");
        }




    }
}