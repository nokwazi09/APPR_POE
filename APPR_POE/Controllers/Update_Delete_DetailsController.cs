using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace APPR_POE.Controllers
{
    public class Update_Delete_DetailsController : Controller
    {
        string conn = "Server=tcp:disasteraid.database.windows.net,1433;Initial Catalog=DisasterAid;Persist Security Info=False;User ID=admin1;Password=jORDENSEOR64;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        // GET: Update_Delete_Details
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonentaryDelete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM MonetaryDonations WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Deleted successfully
                        TempData["Message"] = "Deleted Successfully!!!!!!";
                    }
                    else
                    {
                        // No record found with the given Id
                        TempData["Message"] = "No record found for deletion.";
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or display a generic error message
                TempData["Message"] = "An error occurred while deleting the record: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
