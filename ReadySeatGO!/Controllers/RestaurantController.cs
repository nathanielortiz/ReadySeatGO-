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
                    @RSG_Branch, @RSG_OperatingHours,@RSG_Status , @RSG_Image, @RSG_TotalSeats, 
                    @RSG_DateAdded, @RSG_DateModified)


                    UPDATE RSG_Users SET RSG_UserTypeID=3 WHERE RSG_UserID=@RSG_UserID";

            

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
                    WickedEye.Parameters.AddWithValue("@RSG_Status", "Empty");
                    WickedEye.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                    WickedEye.Parameters.AddWithValue("@RSG_DateModified", DBNull.Value);
                    WickedEye.ExecuteNonQuery();

                    // Redirect to index
                    return RedirectToAction("OwnerHome","Home");

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
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult EditRestaurant(int? id)
        {

           
            if (Session["userid"] == null)
                return RedirectToAction("Login", "Home");
            if (int.Parse(Session["typeid"].ToString()) != 3)
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }


            var rec = new RestaurantModel();
            rec.Categories = GetCategories();
            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"SELECT RSG_RName,RSG_Address,RSG_ContactNumber
                ,RSG_Manager,RSG_Branch,RSG_OperatingHours,
                RSG_Status,RSG_Image,RSG_TotalSeats FROM RSG_Restaurants WHERE RSG_RID =@RG";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@RG", id);
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                rec.Restaurant = dr["RSG_RName"].ToString();
                                rec.Address = dr["RSG_Address"].ToString();
                                rec.Phone = dr["RSG_ContactNumber"].ToString();
                                rec.Manager = dr["RSG_Manager"].ToString();
                                rec.Branch = dr["RSG_Branch"].ToString();
                                rec.OperatingHours = dr["RSG_OperatingHours"].ToString();
                                rec.Status = dr["RSG_Status"].ToString();
                                rec.Image = dr["RSG_Image"].ToString();
                                rec.TotalSeats = dr["RSG_TotalSeats"].ToString();

                            }
                            return View(rec);
                        }
                        else
                        {
                            return RedirectToAction("OwnerHome", "Home");
                        }
                       
                        
                    }

                }
            }
        }
        [HttpPost]
        public ActionResult EditRestaurant(RestaurantModel rec,int? id, HttpPostedFileBase image)
        {
            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE RSG_Restaurants SET RSG_CatID=@RCA, RSG_RName=@RN,RSG_Address=@RA,RSG_ContactNumber=@RC,
                               RSG_Manager=@RM, RSG_Branch=@RB, RSG_OperatingHours=@RO,RSG_Status=@RS,RSG_Image=@RI,     
                               RSG_TotalSeats=@RTS,RSG_DateModified=@RD WHERE RSG_RID=@RID";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@RCA", rec.CatID);
                    com.Parameters.AddWithValue("@RN", rec.Restaurant);
                    com.Parameters.AddWithValue("@RA", rec.Address);
                    com.Parameters.AddWithValue("@RC", rec.Phone);
                    com.Parameters.AddWithValue("@RM", rec.Manager);
                    com.Parameters.AddWithValue("@RB", rec.Branch);
                    com.Parameters.AddWithValue("@RO", rec.OperatingHours);
                    com.Parameters.AddWithValue("@RS", rec.Status);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss-") +
                      image.FileName;
                    com.Parameters.AddWithValue("@RI", fileName);


                    // Upload the chosen file to images > products
                    image.SaveAs(Server.MapPath("~/Images/Restaurants/" + fileName));
                    com.Parameters.AddWithValue("@RTS", rec.TotalSeats);
                    com.Parameters.AddWithValue("@RD", DateTime.Now);
                    com.Parameters.AddWithValue("@RID", id);
                    com.ExecuteNonQuery();
                    ViewBag.Success = "<div class='alert alert-success col-lg-6'>Profile Updated </div>";
                    return RedirectToAction("ViewOwnedRestaurant","RestaurantView");
                    

                }
            }
        }

    }


}