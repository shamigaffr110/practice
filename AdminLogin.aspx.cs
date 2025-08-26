using System;
using System.Data.SqlClient;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = "SELECT admin_id FROM Admins WHERE username = @u AND password = @p";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password); 

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        Session["admin_id"] = Convert.ToInt32(result);
                        Session["admin_user"] = username;
                        Response.Redirect("AdminDashboard.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "❌ Invalid credentials. Please try again.";
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "⚠️ Error: " ;
            }
        }
    }
}
