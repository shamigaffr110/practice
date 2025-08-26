using System;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            string email = txtEmail.Text.Trim();
            string password = txtPass.Text.Trim();

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    string query = "SELECT user_id, name FROM Users WHERE email = @e AND password = @p";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@p", password);  

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Session["user_id"] = Convert.ToInt32(dr["user_id"]);
                            Session["user_name"] = dr["name"].ToString();
                            Response.Redirect("UserDashboard.aspx");
                        }
                        else
                        {
                            lblMsg.CssClass = "text-danger";
                            lblMsg.Text = " Invalid email or password.";
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = " Error " ;
            }
        }
    }
}
