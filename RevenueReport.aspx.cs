using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class RevenueReport : System.Web.UI.Page
    {
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            lblTotal.Text = "";
            try
            {
                DateTime fromDate = DateTime.MinValue;
                DateTime toDate = DateTime.MaxValue;

                if (!DateTime.TryParse(txtFrom.Text.Trim(), out fromDate))
                    fromDate = DateTime.MinValue;

                if (!DateTime.TryParse(txtTo.Text.Trim(), out toDate))
                    toDate = DateTime.MaxValue;

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT payment_id, consumer_number, amount, payment_date, method, status 
                        FROM Payments
                        WHERE payment_date BETWEEN @from AND @to";

                    if (!string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        query += @"
                          AND consumer_number IN (
                            SELECT consumer_number 
                            FROM ElectricityBill 
                            WHERE consumer_name LIKE @name
                          )";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@from", fromDate);
                        cmd.Parameters.AddWithValue("@to", toDate);

                        if (!string.IsNullOrWhiteSpace(txtName.Text))
                        {
                            cmd.Parameters.AddWithValue("@name", "%" + txtName.Text.Trim() + "%");
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            grid.DataSource = dt;
                            grid.DataBind();

                            double totalRevenue = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (double.TryParse(row["amount"].ToString(), out double amt))
                                    totalRevenue += amt;
                            }

                            lblTotal.Text = $"Total Revenue: Rs. {totalRevenue:N2}";
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblTotal.CssClass = "text-danger";
                lblTotal.Text = "Error loading report " ;
            }
        }
    }
}
