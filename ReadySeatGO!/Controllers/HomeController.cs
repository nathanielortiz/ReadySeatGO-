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
    public class HomeController : Controller
    {

        bool IsExisting(string username)
        {
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT RSG_Username FROM RSG_Users
                    WHERE RSG_Username=@RSG_Username";
                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@RSG_Username", username);
                    return WickedEye.ExecuteScalar() == null ? false : true;
                }
            }
        }

        public ActionResult SignUp()
        {
            UsersModel Chuu2 = new UsersModel();
            return View(Chuu2);
        }


        [HttpPost]
        public ActionResult SignUp(UsersModel Chuu2)
        {
            if (IsExisting(Chuu2.Username))
            {
                ViewBag.Error = "<div class='alert alert-danger'>Username is already taken!</div>";
                return View();
            }
            else
            {
                using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
                {
                    Rikka.Open();
                    string Takanashi = @"INSERT INTO RSG_Users VALUES
                    (@RSG_UserTypeID, @RSG_Username, @RSG_UPassword, @RSG_Email, 
                   @RSG_FirstName, @RSG_LastName,
                   @RSG_Address, @RSG_Status, @RSG_Mobile, 
                    @RSG_DateAdded, @RSG_DateModified)";
                    using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                    {

                        WickedEye.Parameters.AddWithValue("@RSG_UPassword", Chuu2.UserPassword);
                        WickedEye.Parameters.AddWithValue("@RSG_UserName", Chuu2.Username);
                        WickedEye.Parameters.AddWithValue("@RSG_Email", Chuu2.Email);
                        WickedEye.Parameters.AddWithValue("@RSG_FirstName", Chuu2.FirstName);
                        WickedEye.Parameters.AddWithValue("@RSG_LastName", Chuu2.LastName);
                        WickedEye.Parameters.AddWithValue("@RSG_Address", Chuu2.Address);
                        WickedEye.Parameters.AddWithValue("@RSG_Status", "Active");
                        WickedEye.Parameters.AddWithValue("@RSG_Mobile", Chuu2.Mobile);
                        WickedEye.Parameters.AddWithValue("@RSG_DateAdded", DateTime.Now);
                        WickedEye.Parameters.AddWithValue("@RSG_DateModified", DBNull.Value);
                        WickedEye.Parameters.AddWithValue("@RSG_UserTypeID", 1);

                        WickedEye.ExecuteNonQuery();
                        return RedirectToAction("Home");
                    }
                }

            }
        }


        //Default Homepage when not logged in
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Sample()
        {
            return View();
        }

        //HomePage Variations between Users
        //Different Users have different homepages

        //Patron - UserType #1
        //Should display Patron's favorite restaurants
        public ActionResult UserHome()
        {
            if (Session["userid"] == null) // user has not logged in
                return RedirectToAction("Login");

            var list = new List<RestaurantModel>();

            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string cheese = @"SELECT RSG_RID ,RSG_Image FROM RSG_Restaurants WHERE RSG_IsFeatured= 'Yes'";
                using (SqlCommand com = new SqlCommand(cheese, con))
                {
                    //com.Parameters.AddWithValue("@Is", "Yes");
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new RestaurantModel
                            {
                                Image = dr["RSG_Image"].ToString(),
                                RestaurantID = int.Parse(dr["RSG_RID"].ToString())
                            });
                        }
                    }
                }
            }
            
            return View("HomePageUser",list);
        }

        //Admin - UserType #2
        //Should display notifications
        public ActionResult AdminHome()
        {
            if (Session["userid"] == null) // user has not logged in
                return RedirectToAction("Login");

            return View("HomePageAdmin");
        }

        //Restaurant Owner - UserType #3
        //Should display favorite Restaurants AND Own restaurant
        public ActionResult OwnerHome()
        {
            if (Session["userid"] == null) // user has not logged in
                return RedirectToAction("Login");
            var list = new List<RestaurantModel>();

            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string cheese = @"SELECT RSG_RID ,RSG_Image FROM RSG_Restaurants WHERE RSG_IsFeatured= 'Yes'";
                using (SqlCommand com = new SqlCommand(cheese, con))
                {
                    //com.Parameters.AddWithValue("@Is", "Yes");
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new RestaurantModel
                            {
                                Image = dr["RSG_Image"].ToString(),
                                RestaurantID = int.Parse(dr["RSG_RID"].ToString())
                            });
                        }
                    }
                }
            }

            return View("HomePageOwner", list);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UsersModel Chuu2)
        {

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT RSG_UserID, RSG_UserTypeID FROM RSG_Users
                    WHERE RSG_Username=@UN AND RSG_UPassword=@PW
                    AND RSG_Status!=@Status";
                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@UN", Chuu2.Username);
                    WickedEye.Parameters.AddWithValue("@PW", Chuu2.UserPassword);
                    WickedEye.Parameters.AddWithValue("@Status", "Archived");

                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        if (Nibutani.HasRows)
                        {
                            while (Nibutani.Read())
                            {
                                Session["userid"] = Nibutani["RSG_UserID"].ToString();
                                Session["typeid"] = Nibutani["RSG_UserTypeID"].ToString();


                            }

                            if (Session["typeid"].ToString() == "1")
                            {
                                return RedirectToAction("UserHome");

                            }
                            else if (Session["typeid"].ToString() == "3")
                            {
                                return RedirectToAction("OwnerHome");
                            }
                            else
                            {
                                return RedirectToAction("AdminHome");
                            }

                        }
                        else
                        {
                            ViewBag.Error =
                                "<div class='alert alert-danger col-lg-6'>Invalid credentials.</div>";
                            return View();
                        }
                    }
                }

            }
        }
        //public ActionResult UserHome()
        //{
        //    var list = new List<RestaurantModel>();
        //    using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
        //    {
        //        con.Open();
        //        string cheese = @"SELECT RSG_Image FROM RSG_Restaurants WHERE RSG_IsFeatured= 'Yes'";
        //        using (SqlCommand com = new SqlCommand(cheese, con))
        //        {
        //            //com.Parameters.AddWithValue("@Is", "Yes");
        //            using (SqlDataReader dr = com.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    list.Add(new RestaurantModel
        //                    {
        //                        Image = dr["RSG_Image"].ToString()
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return View(list);

        //}


    }
}