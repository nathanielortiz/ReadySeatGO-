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

        // View User List
        public ActionResult UserList()
        {
            var list = new List<UsersModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT u.RSG_UserID, t.RSG_Description, u.RSG_Username, u.RSG_Email, u.RSG_LastName,
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
                                UserID = int.Parse(Nibutani["RSG_UserID"].ToString()),
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

        // View User Details
        public ActionResult UserDetails(int? id)
        {
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                if (id == null)
                {
                    return RedirectToAction("UserList");

                }

                var record = new UsersModel();
                using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
                {
                    con.Open();
                    string SQL = @"SELECT u.RSG_Username, t.RSG_Description, u.RSG_Email, u.RSG_FirstName, u.RSG_LastName,
                                u.RSG_Address, u.RSG_Status, u.RSG_Mobile FROM RSG_Users u INNER JOIN RSG_UserTypes t ON 
                                t.RSG_UserTypeID = u.RSG_UserTypeID
                               WHERE u.RSG_UserID = @RSG_UserID";

                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        cmd.Parameters.AddWithValue("@RSG_UserID", id);
                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            if (data.HasRows)
                            {
                                while (data.Read())
                                {
                                    record.Username = data["RSG_Username"].ToString();
                                    record.FirstName = data["RSG_FirstName"].ToString();
                                    record.LastName = data["RSG_LastName"].ToString();
                                    record.Address = data["RSG_Address"].ToString();
                                    record.Email = data["RSG_Email"].ToString();
                                    record.Status = data["RSG_Status"].ToString();

                                    record.UserType = data["RSG_Description"].ToString();
                                    record.Mobile = data["RSG_Mobile"].ToString();

                                }

                                return View(record);
                            }
                            else
                            {
                                return RedirectToAction("UserList");
                            }
                        }
                    }
                }
            }

        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login","Home");
        }

        // Restaurant List
        public ActionResult RestaurantList()
        {
            var list = new List<RestaurantModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT r.RSG_RID, r.RSG_RName, r.RSG_IsFeatured, r.RSG_Address, r.RSG_ContactNumber, r.RSG_OperatingHours,
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
                                IsFeatured = Nibutani["RSG_IsFeatured"].ToString(),
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

        // Set Restaurant as Featured
        public ActionResult SetFeatured(int? id)
        {
            if (id == null)
                return RedirectToAction("RestaurantList");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"UPDATE RSG_Restaurants SET RSG_IsFeatured=@RSG_IsFeatured 
                    WHERE RSG_RID=@RSG_RID";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_IsFeatured", "Yes");
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("RestaurantList");
        }

        // Remove Restaurant as Featured

        public ActionResult RemoveFeatured(int? id)
        {
            if (id == null)
                return RedirectToAction("RestaurantList");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"UPDATE RSG_Restaurants SET RSG_IsFeatured=@RSG_IsFeatured 
                    WHERE RSG_RID=@RSG_RID";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_IsFeatured", "No");
                    cmd.Parameters.AddWithValue("@RSG_RID", id);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("RestaurantList");
        }


        // View Pending Restaurant Application List
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

        // Approve Restaurant Application from Patron (User Type)
        // If Approved -> User Type will change from Patron to Owner
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


        // Reject Restaurant Application from Patron (User Type)
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


        // Add Admin(s)
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

        public ActionResult AddAdmin()
        {
            UsersModel Chuu2 = new UsersModel();
            return View(Chuu2);
        }

        [HttpPost]
        public ActionResult AddAdmin(UsersModel Chuu2)
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
                        WickedEye.Parameters.AddWithValue("@RSG_UserTypeID", 2);

                        WickedEye.ExecuteNonQuery();
                        return RedirectToAction("AdminList");
                    }
                }

            }
        }


        // View Admin List
        public ActionResult AdminList()
        {
            var list = new List<UsersModel>();
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"SELECT u.RSG_UserID, t.RSG_Description, u.RSG_Username, u.RSG_Email, u.RSG_LastName,
                    u.RSG_FirstName, u.RSG_Address, u.RSG_Status, u.RSG_Mobile
                    FROM RSG_Users u
                    INNER JOIN RSG_UserTypes t ON u.RSG_UserTypeID = t.RSG_UserTypeID
                    WHERE u.RSG_Status!=@Status AND u.RSG_UserTypeID = 2";
                using (SqlCommand WickedEye = new SqlCommand(Takanashi, Rikka))
                {
                    WickedEye.Parameters.AddWithValue("@Status", "Archived");
                    using (SqlDataReader Nibutani = WickedEye.ExecuteReader())
                    {
                        while (Nibutani.Read())
                        {
                            list.Add(new UsersModel
                            {
                                UserID = int.Parse(Nibutani["RSG_UserID"].ToString()),
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

        //View Admin Details
        public ActionResult AdminDetails(int? id)
        {
            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                if (id == null)
                {
                    return RedirectToAction("UserList");

                }

                var record = new UsersModel();
                using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
                {
                    con.Open();
                    string SQL = @"SELECT u.RSG_Username, t.RSG_Description, u.RSG_Email, u.RSG_FirstName, u.RSG_LastName,
                                u.RSG_Address, u.RSG_Status, u.RSG_Mobile FROM RSG_Users u INNER JOIN RSG_UserTypes t ON 
                                t.RSG_UserTypeID = u.RSG_UserTypeID
                                WHERE u.RSG_UserID = @RSG_UserID";

                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        cmd.Parameters.AddWithValue("@RSG_UserID", id);
                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            if (data.HasRows)
                            {
                                while (data.Read())
                                {
                                    record.Username = data["RSG_Username"].ToString();
                                    record.FirstName = data["RSG_FirstName"].ToString();
                                    record.LastName = data["RSG_LastName"].ToString();
                                    record.Address = data["RSG_Address"].ToString();
                                    record.Email = data["RSG_Email"].ToString();
                                    record.Status = data["RSG_Status"].ToString();

                                    record.UserType = data["RSG_Description"].ToString();
                                    record.Mobile = data["RSG_Mobile"].ToString();

                                }

                                return View(record);
                            }
                            else
                            {
                                return RedirectToAction("UserList");
                            }
                        }
                    }
                }
            }

        }

        // Archive Admin
        public ActionResult ArchiveAdmin(int? id)
        {
            if (id == null)
                return RedirectToAction("AdminList");

            using (SqlConnection Rikka = new SqlConnection(Dekomori.GetConnection()))
            {
                Rikka.Open();
                string Takanashi = @"UPDATE RSG_Users SET RSG_Status=@RSG_Status
                    WHERE RSG_UserID=@RSG_UserID";
                using (SqlCommand cmd = new SqlCommand(Takanashi, Rikka))
                {
                    cmd.Parameters.AddWithValue("@RSG_Status", "Archived");
                    cmd.Parameters.AddWithValue("@RSG_UserID", id);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("AdminList");
        }



    }
}
