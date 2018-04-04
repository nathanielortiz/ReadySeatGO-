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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var list = new List<UsersModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT t.RSG_Description, u.RSG_Username, u.RSG_Email, u.RSG_LastName,
                    u.RSG_FirstName, u.RSG_Address, u.RSG_Status, u.RSG_Mobile
                    FROM RSG_Users u
                    INNER JOIN RSG_UserTypes t ON u.RSG_UserTypeID = t.RSG_UserTypeID
                    WHERE u.RSG_Status!=@Status";
                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@Status", "Archived");
                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new UsersModel
                            {

                                UserType = Nibutani["RSG_Description"].ToString(),
                                Username = Nibutani["RSG_Username"].ToString(),
                                Email = Nibutani["RSG_Email"].ToString(),
                                LastName = Nibutani["RSG_LastName"].ToString(),
                                FirstName = Nibutani["RSG_FirstName"].ToString(),
                                Address = Nibutani["RSG_Address"].ToString(),
                                Mobile = Nibutani["RSG_Mobile"].ToString(),
                                Status = Nibutani["RSG_Status"].ToString(),

                            });
                        }
                    }
                }
            }

            return View(list);
        }

        public ActionResult PendingRestaurantApplication()
        {
            var list = new List<RestaurantModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT r.RSG_RID, r.RSG_RName, r.RSG_Address, r.RSG_ContactNumber, r.RSG_OperatingHours,
                    r.RSG_Manager, r.RSG_Branch, r.RSG_TotalSeats,
                    u.RSG_Username, c.RSG_Category, a.RSG_Description
                    FROM RSG_Restaurants AS r
                    INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID
                    INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID
                   INNER JOIN RSG_ApprovalStatus a ON r.RSG_ApprovalID = a.RSG_ApprovalID
				   WHERE r.RSG_ApprovalID = 1";
                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {

                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new RestaurantModel
                            {
                                RestaurantID = int.Parse(Nibutani["RSG_RID"].ToString()),
                                UserName = Nibutani["RSG_UserName"].ToString(),
                                Restaurant = Nibutani["RSG_RName"].ToString(),
                                Address = Nibutani["RSG_Address"].ToString(),
                                Phone = Nibutani["RSG_ContactNumber"].ToString(),
                                Manager = Nibutani["RSG_Manager"].ToString(),
                                Branch = Nibutani["RSG_Branch"].ToString(),
                                TotalSeats = Nibutani["RSG_TotalSeats"].ToString(),
                                ApprovalStatusID = Nibutani["RSG_Description"].ToString(),
                                OperatingHours = Nibutani["RSG_OperatingHours"].ToString()

                            });
                        }
                    }
                }
            }

            return View(list);
        }


        
        public ActionResult RestaurantList()
        {
            var list = new List<RestaurantModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT r.RSG_RID, r.RSG_RName, r.RSG_Address, r.RSG_ContactNumber, r.RSG_OperatingHours,
                    r.RSG_Manager, r.RSG_Branch, r.RSG_TotalSeats,
                    u.RSG_Username, c.RSG_Category, a.RSG_Description
                    FROM RSG_Restaurants AS r
                    INNER JOIN RSG_Users u ON r.RSG_UserID = u.RSG_UserID
                    INNER JOIN RSG_Categories c ON r.RSG_CatID = c.RSG_CatID
                   INNER JOIN RSG_ApprovalStatus a ON r.RSG_ApprovalID = a.RSG_ApprovalID
				   ";

                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {

                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new RestaurantModel
                            {
                                RestaurantID = int.Parse(Nibutani["RSG_RID"].ToString()),
                                UserName = Nibutani["RSG_UserName"].ToString(),
                                Restaurant = Nibutani["RSG_RName"].ToString(),
                                Address = Nibutani["RSG_Address"].ToString(),
                                Phone = Nibutani["RSG_ContactNumber"].ToString(),
                                Manager = Nibutani["RSG_Manager"].ToString(),
                                Branch = Nibutani["RSG_Branch"].ToString(),
                                TotalSeats = Nibutani["RSG_TotalSeats"].ToString(),
                                ApprovalStatusID = Nibutani["RSG_Description"].ToString(),
                                OperatingHours = Nibutani["RSG_OperatingHours"].ToString()

                            });
                        }
                    }
                }
            }

            return View(list);
        }


        

        public ActionResult ApproveApplication(int? id)
        {
            if (id == null)
                return RedirectToAction("RestaurantList");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"UPDATE RSG_Restaurants SET RSG_ApprovalID=@RSG_ApprovalID, 
                    RSG_DateAdded=@DateAdded
                    WHERE RSG_RID=@RSG_RID";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_ApprovalID", 2);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("RestaurantList");
        }

        public ActionResult RejectApplication(int? id)
        {
            if (id == null)
                return RedirectToAction("RestaurantList");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"UPDATE RSG_Restaurants SET RSG_ApprovalID=@RSG_ApprovalID, 
                    RSG_DateAdded=@DateAdded
                    WHERE RSG_RID=@RSG_RID";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_ApprovalID", 3);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("RestaurantList");
        }






    }
}
