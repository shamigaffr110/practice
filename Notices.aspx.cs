using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;
using ElectricityBillProject.Models;

namespace ElectricityBillProject
{
    public partial class Notices : System.Web.UI.Page
    {
        protected void btnFind_Click(object sender, EventArgs e)
        {
            lblMsg.CssClass = "";
            lblMsg.Text = "";

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT consumer_number, consumer_name, bill_amount, bill_date, status
                        FROM ElectricityBill
                        WHERE status = 'Unpaid'
                          AND bill_amount > 200000
                          AND DATEDIFF(day, bill_date, GETDATE()) > 365";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        grid.DataSource = dt;
                        grid.DataBind();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error loading data " ;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblMsg.CssClass = "";
            lblMsg.Text = "";

            string consumerNumber = txtConsumer.Text.Trim();

            if (string.IsNullOrEmpty(consumerNumber))
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Please enter a valid consumer number.";
                return;
            }

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    // Insert legal notice record
                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Notices (consumer_number, message, created_at)
                        VALUES (@c, @m, GETDATE())", con))
                    {
                        cmd.Parameters.AddWithValue("@c", consumerNumber);
                        cmd.Parameters.AddWithValue("@m", "Legal notice: Please clear dues immediately.");
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                // Attempt to find consumer email and send notice email
                string emailResult = "(Email not sent)";

                using (SqlConnection con2 = DBHandler.GetConnection())
                {
                    con2.Open();

                    string emailQuery = @"
                        SELECT TOP 1 email
                        FROM Connections
                        WHERE consumer_number = @c
                        ORDER BY created_at DESC";

                    using (SqlCommand cmd = new SqlCommand(emailQuery, con2))
                    {
                        cmd.Parameters.AddWithValue("@c", consumerNumber);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            string email = result.ToString();

                            string subject = "Legal Notice - Electricity Dues";
                            string body = "<p>Dear Customer,<br/>" +
                                          "Your electricity dues exceed Rs. 2,00,000 and are pending for over a year.<br/>" +
                                          "Please clear your bill immediately to avoid further action.</p>";

                            emailResult = EmailService.SendMail(email, subject, body);
                        }
                    }

                    con2.Close();
                }

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Notice recorded " ;
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error sending notice " ;
            }
        }
    }
}
