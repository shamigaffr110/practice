using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ElectricityBillProject.Models;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int uid = Convert.ToInt32(Session["user_id"] ?? 0);
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();
                    var cmd = new SqlCommand("SELECT name,email,phone FROM Users WHERE user_id=@u", con);
                    cmd.Parameters.AddWithValue("@u", uid);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtName.Text = dr["name"].ToString();
                            txtEmail.Text = dr["email"].ToString();
                            txtPhone.Text = dr["phone"].ToString();
                        }
                    }
                    con.Close();
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int uid = Convert.ToInt32(Session["user_id"]);
            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();
                var cmd = new SqlCommand("UPDATE Users SET name=@n,email=@e,phone=@p WHERE user_id=@u", con);
                cmd.Parameters.AddWithValue("@u", uid);
                cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@e", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@p", txtPhone.Text.Trim());
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Profile updated successfully!";
                con.Close();
            }
        }
    }
}
