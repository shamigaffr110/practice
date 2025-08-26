using System;
using System.Data.SqlClient;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int uid = Convert.ToInt32(Session["user_id"] ?? 0);
                if (uid == 0)
                {
                    Response.Redirect("UserLogin.aspx"); 
                    return;
                }

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();
                    var cmd = new SqlCommand(@"
                        SELECT 
                            ISNULL(SUM(bill_amount), 0) AS TotalDue
                        FROM ElectricityBill 
                        WHERE consumer_number IN
                          (SELECT consumer_number FROM Connections WHERE user_id=@u) 
                          AND status='Unpaid';

                        SELECT TOP 1 payment_date FROM Payments WHERE user_id=@u ORDER BY payment_date DESC;", con);
                    cmd.Parameters.AddWithValue("@u", uid);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            lblDue.Text = "₹" + dr["TotalDue"].ToString();
                        }
                        if (dr.NextResult() && dr.Read())
                        {
                            lblLastPay.Text = dr.GetDateTime(0).ToString("yyyy-MM-dd HH:mm");
                        }
                        else
                        {
                            lblLastPay.Text = "--";
                        }
                    }
                    con.Close();
                }
            }
        }
    }
}
