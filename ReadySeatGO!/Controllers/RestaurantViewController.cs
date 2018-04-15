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
                    Takanashi = "SELECT r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, r.RSG_TotalSeats, r.RSG_Status, c.RSG_Category FROM RSG_Restaurants r " +
                                "INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID " +
                                "INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID";
                }

                else
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours,r.RSG_TotalSeats, r.RSG_Status, c.RSG_Category FROM RSG_Restaurants r " +
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
                                TotalSeats = Nibutani["RSG_TotalSeats"].ToString(),
                                Status = Nibutani["RSG_Status"].ToString()

                            });
                        }


                    }
                    



                }


            }

            return list;
        }


        //public List<CheckInModel> GetCheckIns()
        //{
        //    var list = new List<CheckInModel>();
        //    using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
        //    {
        //        Rikka.Open();
        //        string Takanashi = @"SELECT COUNT (RSG_LogID) AS TotalCheckIn FROM RSG_CheckIn WHERE RSG_RID=@RSG_ID";
        //        using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
        //        {
        //            using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
        //            {
        //                while (Nibutani.Read())
        //                {
        //                    list.Add(new CheckInModel
        //                    {
        //                        CheckInID = int.Parse(Nibutani["RSG_LogID"].ToString()),
        //                        TotalCheckIn = int.Parse(Nibutani["TotalCheckIn"].ToString()),
        //                        RestaurantID = int.Parse(Nibutani["RSG_RID"].ToString())

        //                    });
        //                }
        //            }
        //            return list;

        //        }

        //    }
        //}

        

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






        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");

            }

            var Chuu2 = new CheckStatusModel();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT r.RSG_CheckStatID, r.RSG_RID, res.RSG_RName, res.RSG_Image, res.RSG_RName, u.RSG_Username, 
                               res.RSG_Address, res.RSG_ContactNumber, res.RSG_Branch,
                               res.RSG_OperatingHours,res.RSG_TotalSeats, c.RSG_Category FROM RSG_CheckStat r 
							   INNER JOIN RSG_Restaurants res ON r.RSG_RID = res.RSG_RID
                               INNER JOIN RSG_Categories c ON res.RSG_CatID = c.RSG_CatID
                               INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID 
                               WHERE r.RSG_RID = @RID";



                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RID", id);
                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        if (Nibutani.HasRows)
                        {
                            while (Nibutani.Read())
                            {
                                Chuu2.RestaurantID = int.Parse(Nibutani["RSG_RID"].ToString());
                                Chuu2.RestaurantName = Nibutani["RSG_RName"].ToString();
                                Chuu2.Address = Nibutani["RSG_Address"].ToString();
                                Chuu2.Image = Nibutani["RSG_Image"].ToString();
                                Chuu2.Branch = Nibutani["RSG_Branch"].ToString();
                                Chuu2.ContactNumber = Nibutani["RSG_ContactNumber"].ToString();
                                Chuu2.Owner = Nibutani["RSG_Username"].ToString();
                                Chuu2.TotalSeats = Nibutani["RSG_TotalSeats"].ToString();
                                Chuu2.OperatingHours = Nibutani["RSG_OperatingHours"].ToString();
                                Chuu2.Category = Nibutani["RSG_Category"].ToString();
                                Chuu2.CheckStatusID = int.Parse(Nibutani["RSG_CheckStatID"].ToString());

                            }


                            ViewBag.Total = (GetTotalCheckIns(id).ToString());

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

        // View Raing
        public ActionResult ViewAVGRating(int id)
        {
            var list = new List<RatingsModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT AVG(RSG_Cleanliness) AS Cleanliness, AVG(RSG_CustomerService) AS CustomerService, AVG(RSG_FoodQuality) AS FoodQuality FROM RSG_Ratings WHERE RSG_RID = @RSG_RID";

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RSG_RID", id);
                    using (SqlDataReader data = WickedEye.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new RatingsModel
                            {
                                Cleanliness = int.Parse(data["Cleanliness"].ToString()),
                                CustomerService = int.Parse(data["CustomerService"].ToString()),
                                FoodQuality = int.Parse(data["FoodQuality"].ToString()),


                            });
                        }
                    }

                }
                return View(list);
            }
        }



        // View Customer Reviews
        public ActionResult ViewRatings(int? id)
        {
            var list = new List<RatingsModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT r.RSG_Cleanliness, r.RSG_CustomerService, r.RSG_FoodQuality, r.RSG_Remarks, u.RSG_Username FROM RSG_Ratings r INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID
                                    WHERE r.RSG_RID = @RSG_RID";

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RSG_RID", id);
                    using (SqlDataReader data = WickedEye.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new RatingsModel
                            {

                                Cleanliness = int.Parse(data["RSG_Cleanliness"].ToString()),
                                CustomerService = int.Parse(data["RSG_CustomerService"].ToString()),
                                FoodQuality = int.Parse(data["RSG_FoodQuality"].ToString()),
                                Remarks = data["RSG_Remarks"].ToString(),
                                User = data["RSG_Username"].ToString()



                            });
                        }
                    }

                }

            }
            return View(list);

        }

        // Check-In for Patron(s)
        public ActionResult CheckIn(int? id)
        {
            if (id == null)
                return RedirectToAction("Favor");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"INSERT INTO RSG_CheckIn VALUES (@RSG_UserID, @RSG_RID, @RSG_Remarks, @RSG_DateAdded)
                                     
                                    UPDATE RSG_CheckStat SET RSG_RID=@RSG_RID, RSG_Description=@RD WHERE RSG_UserID=@RSG_UserID";
                                        

                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_UserID", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.Parameters.AddWithValue("@RSG_Remarks", "Check-in");
                    cmd.Parameters.AddWithValue("@RD", "Checked-in");
                    cmd.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("Index");
        }

        // Check-Out for Patron(s)
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
                return RedirectToAction("Favor");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"INSERT INTO RSG_CheckOut VALUES (@RSG_UserID, @RSG_RID, @RSG_Remarks, @RSG_DateAdded)
                                     
                                    UPDATE RSG_CheckStat SET RSG_RID=@RSG_RID, RSG_Description=@RD WHERE RSG_UserID=@RSG_UserID";


                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_UserID", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.Parameters.AddWithValue("@RSG_Remarks", "Check-in");
                    cmd.Parameters.AddWithValue("@RD", "Checked-out");
                    cmd.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("Index");
        }

        int GetTotalCheckIns(int? id)
        {
            int Total = 0;
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT Count(RSG_CheckStatID) AS TOTAL FROM RSG_CheckStat WHERE RSG_Description = @RD AND RSG_RID =@RID";

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RID", id);
                    WickedEye.Parameters.AddWithValue("@RD", "Checked-in");

                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        if(Nibutani.HasRows)
                        {
                            while(Nibutani.Read())
                            {
                                Total = int.Parse(Nibutani["TOTAL"].ToString());
                            }
                        }
                    }

                }
            }
            return Total;
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
            return RedirectToAction("ViewFavorites", "RestaurantView");
        }

        // Add Rating
        public ActionResult AddRating()
        {
            RatingsModel Chuu2 = new RatingsModel();
            return View(Chuu2);
        }

        [HttpPost]
        public ActionResult AddRating(RatingsModel Chuu2, int? id)
        {
                using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
                {
                    Rikka.Open();
                     string Takanashi = @"INSERT INTO RSG_Ratings VALUES (@RSG_RID, @RSG_UserID, @RSG_Cleanliness, @RSG_CustomerService, @RSG_FoodQuality, @RSG_Remarks, @RSG_DateAdded)";
                    using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                    {
                        WickedEye.Parameters.AddWithValue("@RSG_RID", id);
                         WickedEye.Parameters.AddWithValue("@RSG_UserID", Session["userid"].ToString());
                        
                        WickedEye.Parameters.AddWithValue("@RSG_Cleanliness", Chuu2.Cleanliness);
                        WickedEye.Parameters.AddWithValue("@RSG_CustomerService", Chuu2.CustomerService);
                        WickedEye.Parameters.AddWithValue("@RSG_FoodQuality", Chuu2.FoodQuality);
                        WickedEye.Parameters.AddWithValue("@RSG_Remarks", Chuu2.Remarks);
                        WickedEye.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                        
                        WickedEye.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                }

            }
        

        // View Favorites
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

        public List<RestaurantModel> GetFavoriteRestaurants()
        {

            var list = new List<RestaurantModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"";
                if (Request.QueryString["c"] == null)
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_RName, c.RSG_Category, r.RSG_Image, r.RSG_OperatingHours, r.RSG_TotalSeats, r.RSG_Status FROM RSG_Favorites f INNER JOIN RSG_Restaurants r ON f.RSG_RID = r.RSG_RID " +
                                "INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID " +
                                "INNER JOIN RSG_Users u ON f.RSG_UserID = u.RSG_UserID WHERE f.RSG_UserID = @RSG_UID";
                }

                else
                {
                    Takanashi = "SELECT r.RSG_RID, r.RSG_RName, c.RSG_Category, r.RSG_Image, r.RSG_OperatingHours, r.RSG_TotalSeats, r.RSG_Status FROM RSG_Favorites f INNER JOIN RSG_Restaurants r ON f.RSG_RID = r.RSG_RID " +
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
                                TotalSeats = Nibutani["RSG_TotalSeats"].ToString(),
                                Status = Nibutani["RSG_Status"].ToString()

                            });
                        }
                    }
                }


            }

            return list;
        }

        // Remove from Favorites
        public ActionResult RemoveFavorites(int? id)
        {
            if (id == null)
                return RedirectToAction("ViewFavorites");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"DELETE FROM RSG_Favorites WHERE RSG_FavoriteID=@RFID)";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RFID", id);
                   
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("ViewFavorites");
        }



        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        // Remove Favorites
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


        public List<RestaurantModel> GetOwnedRestaurants()
        {
            var list = new List<RestaurantModel>();

            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();

                string ow = "";

                if (Request.QueryString["c"] == null)
                {
                    ow = "SELECT  r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, c.RSG_Category FROM RSG_Restaurants r  INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID WHERE r.RSG_UserID=@RU AND u.RSG_UserTypeID=@RUT";
                }
                else
                {
                    ow = "SELECT  r.RSG_RID, r.RSG_Image, r.RSG_RName, u.RSG_Username, r.RSG_OperatingHours, c.RSG_Category FROM RSG_Restaurants r INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID WHERE r.RSG_UserID = @RU AND u.RSG_UserTypeID = @RUT AND r.RSG_CatID = @RCI";
                }         
                using (SqlCommand com = new SqlCommand(ow, con))
                {
                    com.Parameters.AddWithValue("@RU", Session["userid"].ToString());
                    com.Parameters.AddWithValue("@RUT",Session["typeid"].ToString());
                    com.Parameters.AddWithValue("@RCI", Request.QueryString["c"] == null ? "0" : Request.QueryString["c"].ToString());
                    using (SqlDataReader Nibutani = com.ExecuteReader())
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
        public ActionResult ViewOwnedRestaurant()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var list = new RestaurantViewModel();
                list.Restaurants = GetOwnedRestaurants();
                list.Categories = GetCategories();
                return View(list);
            }

        }




    }
}