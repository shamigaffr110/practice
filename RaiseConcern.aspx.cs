using System;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class RaiseConcern : System.Web.UI.Page
    {
        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblMsg.CssClass = "";
            lblMsg.Text = "";

            try
            {
                // Validate inputs
                string consumerNumber = txtConsumer.Text.Trim();
                string message = txtMessage.Text.Trim();

                if (string.IsNullOrEmpty(consumerNumber))
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Please enter your Consumer Number.";
                    return;
                }

                if (string.IsNullOrEmpty(message))
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Please enter your concern message.";
                    return;
                }

                if (Session["user_id"] == null)
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "You must be logged in to raise a concern.";
                    return;
                }

                int userId = Convert.ToInt32(Session["user_id"]);

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    // Insert concern
                    string query = @"
                        INSERT INTO Concerns(user_id, consumer_number, message, status, created_at)
                        VALUES (@user_id, @consumer_number, @message, 'Open', GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@consumer_number", consumerNumber);
                    cmd.Parameters.AddWithValue("@message", message);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        lblMsg.CssClass = "text-success";
                        lblMsg.Text = "Your concern has been submitted successfully.";
                        txtConsumer.Text = "";
                        txtMessage.Text = "";
                    }
                    else
                    {
                        lblMsg.CssClass = "text-danger";
                        lblMsg.Text = "Failed to submit concern. Please try again.";
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error " ;
            }
        }
    }
}
