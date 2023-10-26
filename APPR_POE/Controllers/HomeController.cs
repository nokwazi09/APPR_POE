using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Windows.Forms;
using APPR_POE.Models;
namespace APPR_POE.Controllers
{
    public class HomeController : Controller
    {
        DisasterAidEntities entities = new DisasterAidEntities();
        string conn = "Server=tcp:disasteraid.database.windows.net,1433;Initial Catalog=DisasterAid;Persist Security Info=False;User ID=admin1;Password=jORDENSEOR64;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MonetaryList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter("Select * from MonetaryDonations", con);
                ad.Fill(dt);
            }
            return View(dt);
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserTable user)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "INSERT INTO UserTable(Email, Full_name, Password)" +
                        "VALUES (@Email, @Full_name, @Password)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Full_name", user.Full_name);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("MonetaryList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }

        [HttpPost]
        public ActionResult LogIn(UserTable user)
        {
            var checkLogin = entities.UserTables.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["Email"] = user.Email.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Incorrect Username or Password";

            }
            return View();
        }
        public ActionResult MonetaryDonations()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MonetaryDonations(MonetaryDonation monetary, UserTable user)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "INSERT INTO MonetaryDonations(EMAILS, DONATORNAME, DATE, Amount)" +
                        "VALUES (@Emails, @DonatorName, @Date, @Amount)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Emails", Session["Email"]);
                        cmd.Parameters.AddWithValue("@DonatorName", monetary.DonatorName);
                        cmd.Parameters.AddWithValue("@Date", monetary.Date);
                        cmd.Parameters.AddWithValue("@Amount", monetary.Amount);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("MonetaryList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }

        }
        public ActionResult ListDisaster()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter("Select * from Disaster", con);
                ad.Fill(dt);
            }

            return View(dt);
        }

        public ActionResult AddDisaster()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDisaster(Disaster disaster)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "INSERT INTO Disaster(Start_date, End_date, Location, Description, Aid_types, Email)" +
                        "VALUES (@Start_date, @End_date, @Location, @Description, @Aid_types, @Email)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", Session["Email"]);
                        cmd.Parameters.AddWithValue("@Start_date", disaster.Start_date);
                        cmd.Parameters.AddWithValue("@End_date", disaster.End_date);
                        cmd.Parameters.AddWithValue("@Location", disaster.Location);
                        cmd.Parameters.AddWithValue("@Description", disaster.Description);
                        cmd.Parameters.AddWithValue("@Aid_types", disaster.Aid_types);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("ListDisaster", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }

        public ActionResult GoodList()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter("Select * from GoodDonations", con);
                ad.Fill(dt);
            }

            return View(dt);
        }


        [HttpGet]
        public ActionResult GoodDonations()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GoodDonations(GoodDonation good)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "INSERT INTO GoodDonations(Email, DonarName, Date, NumItems, Category, Description)" +
                        "VALUES (@Email, @DonarName, @Date, @NumItems, @Category, @Description)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", Session["Email"]);
                        cmd.Parameters.AddWithValue("@DonarName", good.DonarName);
                        cmd.Parameters.AddWithValue("@Date", good.Date);
                        cmd.Parameters.AddWithValue("@NumItems", good.NumItems);
                        cmd.Parameters.AddWithValue("@Category", good.Category);
                        cmd.Parameters.AddWithValue("@Description", good.Description);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("GoodList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult EditMonetary(int id, MonetaryDonation monetary)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string query = "SELECT * FROM MonetaryDonations WHERE MonetaryId = @MonetaryId";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@MonetaryId", id);
                    adapter.Fill(dt);
                }
                if (dt.Rows.Count == 1)
                {
                    monetary.MonetaryId = Convert.ToInt32(dt.Rows[0][0].ToString());
                    monetary.Emails = dt.Rows[0][1].ToString();
                    monetary.DonatorName = dt.Rows[0][2].ToString();
                    monetary.Date = Convert.ToDateTime(dt.Rows[0][3].ToString());
                    monetary.Amount = Convert.ToInt32(dt.Rows[0][4].ToString());
                    return View(monetary);
                }
                else
                {
                    return RedirectToAction("MonetaryList");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult EditMonetary(MonetaryDonation monetary, UserTable user)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "UPDATE MonetaryDonations SET Emails = @Emails, DonatorName = @DonatorName, Date = @Date, Amount = @Amount WHERE MonetaryId = @MonetaryId";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MonetaryId", monetary.MonetaryId);
                        cmd.Parameters.AddWithValue("@Emails", Session["Email"]);
                        cmd.Parameters.AddWithValue("@DonatorName", monetary.DonatorName);
                        cmd.Parameters.AddWithValue("@Date", monetary.Date);
                        cmd.Parameters.AddWithValue("@Amount", monetary.Amount);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("MonetaryList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }

        }
        public ActionResult DeletMonetary(int id)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "DELETE MonetaryDonations WHERE MonetaryId = @MonetaryId";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MonetaryId", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("MonetaryList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }
        public ActionResult MonentaryDeatils(int id, MonetaryDonation monetary)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string query = "SELECT * FROM MonetaryDonations WHERE MonetaryId = @MonetaryId";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@MonetaryId", id);
                    adapter.Fill(dt);
                }
                if (dt.Rows.Count == 1)
                {
                    monetary.MonetaryId = Convert.ToInt32(dt.Rows[0][0].ToString());
                    monetary.Emails = dt.Rows[0][1].ToString();
                    monetary.DonatorName = dt.Rows[0][2].ToString();
                    monetary.Date = Convert.ToDateTime(dt.Rows[0][3].ToString());
                    monetary.Amount = Convert.ToInt32(dt.Rows[0][4].ToString());
                    return View(monetary);
                }
                else
                {
                    return RedirectToAction("MonetaryList");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult EditGoods(int id, GoodDonation good)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string query = "SELECT * FROM MonetaryDonations WHERE MonetaryId = @MonetaryId";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@MonetaryId", id);
                    adapter.Fill(dt);
                }
                if (dt.Rows.Count == 1)
                {
                    good.GoodId = Convert.ToInt32(dt.Rows[0][0].ToString());
                    good.Email = dt.Rows[0][1].ToString();
                    good.DonarName = dt.Rows[0][2].ToString();
                    good.Date = Convert.ToDateTime(dt.Rows[0][3].ToString());
                    good.NumItems = Convert.ToInt32(dt.Rows[0][4].ToString());
                    good.Category = dt.Rows[0][5].ToString();
                    good.Description = dt.Rows[0][6].ToString();
                    return View(good);
                }
                else
                {
                    return RedirectToAction("MonetaryList");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult EditGoods(GoodDonation good)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "UPDATE GoodDonations SET Email = @Email, DonarName = @DonarName, Date = @Date, NumItems = @NumItems, Category = @Category, Description = @Description  WHERE GoodId = @GoodId";

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@GoodId", good.GoodId);
                        cmd.Parameters.AddWithValue("@Email", Session["Email"]);
                        cmd.Parameters.AddWithValue("@DonarName", good.DonarName);
                        cmd.Parameters.AddWithValue("@Date", good.Date);
                        cmd.Parameters.AddWithValue("@NumItems", good.NumItems);
                        cmd.Parameters.AddWithValue("@Category", good.Category);
                        cmd.Parameters.AddWithValue("@Description", good.Description);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("GoodList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }

        }
        public ActionResult DeletGoods(int id)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "DELETE GoodDonations WHERE GoodId = @GoodId";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@GoodId", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted Successfully!!!!!!");
                    }
                    con.Close();
                    return RedirectToAction("MonetaryList", "Home");
                }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }
        public ActionResult GoodDeatils(int id, GoodDonation good)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string query = "SELECT * FROM MonetaryDonations WHERE MonetaryId = @MonetaryId";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@MonetaryId", id);
                    adapter.Fill(dt);
                }
                if (dt.Rows.Count == 1)
                {
                    good.GoodId = Convert.ToInt32(dt.Rows[0][0].ToString());
                    good.Email = dt.Rows[0][1].ToString();
                    good.DonarName = dt.Rows[0][2].ToString();
                    good.Date = Convert.ToDateTime(dt.Rows[0][3].ToString());
                    good.NumItems = Convert.ToInt32(dt.Rows[0][4].ToString());
                    good.Category = dt.Rows[0][5].ToString();
                    good.Description = dt.Rows[0][6].ToString();
                    return View(good);
                }
                else
                {
                    return RedirectToAction("MonetaryList");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        

public class HomeController:Controller:Controller
            {
            public ActionResult AllocatingMoney()
        {
            return View();
        }
            

    }
}