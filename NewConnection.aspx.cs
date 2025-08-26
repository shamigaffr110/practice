using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.IO;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class NewConnection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only process when form is submitted
            if (IsPostBack && Request.Form["txtName"] != null)
            {
                SubmitApplication();
            }
        }

        private void SubmitApplication()
        {
            try
            {
                string idPath = "", photoPath = "";

                // Handle file uploads manually
                var idFile = Request.Files["fuId"];
                if (idFile != null && idFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + Path.GetFileName(idFile.FileName);
                    idPath = "Uploads/IdProof/" + fileName;
                    idFile.SaveAs(Server.MapPath("~/" + idPath));
                }

                var photoFile = Request.Files["fuPhoto"];
                if (photoFile != null && photoFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + Path.GetFileName(photoFile.FileName);
                    photoPath = "Uploads/Photo/" + fileName;
                    photoFile.SaveAs(Server.MapPath("~/" + photoPath));
                }

                int userId = 0;
                if (Session["user_id"] != null)
                    userId = Convert.ToInt32(Session["user_id"]);

                // Read values from HTML inputs
                string name = Request.Form["txtName"];
                string phone = Request.Form["txtPhone"];
                string email = Request.Form["txtEmail"];
                string address = Request.Form["txtAddress"];
                string locality = Request.Form["txtLocality"];
                string type = Request.Form["ddlType"];
                string connType = Request.Form["ddlConnType"];
                int load = Convert.ToInt32(Request.Form["txtLoad"]);

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Connections
                        (user_id, name, phone, email, address, locality, type, load, connection_type, id_proof_path, photo_path, status)
                        VALUES
                        (@u, @n, @p, @e, @a, @l, @t, @ld, @ct, @id, @ph, 'Pending')", con))
                    {
                        cmd.Parameters.AddWithValue("@u", userId);
                        cmd.Parameters.AddWithValue("@n", name);
                        cmd.Parameters.AddWithValue("@p", phone);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@a", address);
                        cmd.Parameters.AddWithValue("@l", locality);
                        cmd.Parameters.AddWithValue("@t", type);
                        cmd.Parameters.AddWithValue("@ld", load);
                        cmd.Parameters.AddWithValue("@ct", connType);
                        cmd.Parameters.AddWithValue("@id", idPath);
                        cmd.Parameters.AddWithValue("@ph", photoPath);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                // Return a message directly in HTML
                Response.Write("<div class='alert alert-success mt-3'>✅ Application submitted successfully. Please pay Rs. 8000 on the Payment page.</div>");
            }
            catch (Exception ex)
            {
                Response.Write("<div class='alert alert-danger mt-3'>❌ Error while submitting application.</div>");
            }
        }
    }
}
