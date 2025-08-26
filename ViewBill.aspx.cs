using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class ViewBill : System.Web.UI.Page
    {
        protected void btnGo_Click(object sender, EventArgs e)
        {
            int n = 5;
            if (!int.TryParse(txtN.Text.Trim(), out n))
            {
                n = 5; // default
            }
            if (n <= 0) n = 5;

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT TOP (@n) consumer_number, consumer_name, units_consumed, bill_amount, bill_date, status
                        FROM ElectricityBill
                        ORDER BY bill_date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@n", n);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            grid.DataSource = dt;
                            grid.DataBind();

                            lblMsg.Text = $"Details of last '{n}' bills:";
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error loading bills";
            }
        }
    }
}
