using System;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;
using ElectricityBillProject.Models;

namespace ElectricityBillProject
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                qrImage.Visible = false;
                btnSubmit.Enabled = false;
                lblStatus.Text = string.Empty;
            }
        }

        protected void btnGenQR_Click(object sender, EventArgs e)
        {
            qrImage.ImageUrl = "~/Styles/myQR.png"; // Ensure this image exists
            qrImage.Visible = true;
            lblStatus.Text = "📱 Scan the QR code to initiate payment.";
            btnSubmit.Enabled = false;

            ScriptManager.RegisterStartupScript(this, GetType(), "showQR", "showQR();", true);
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            bool paymentSuccess = new Random().Next(0, 2) == 1;

            if (paymentSuccess)
            {
                lblStatus.Text = "✅ Payment successful. You may now submit.";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                btnSubmit.Enabled = true;
            }
            else
            {
                lblStatus.Text = "❌ Payment failed. Please try again.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                btnSubmit.Enabled = false;
                qrImage.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int uid = Session["user_id"] != null ? Convert.ToInt32(Session["user_id"]) : 0;
                string consumerNumber = txtConsumer.Text.Trim();
                string method = ddlMethod.SelectedValue;
                double amount;

                if (string.IsNullOrEmpty(consumerNumber) || !double.TryParse(txtAmount.Text, out amount))
                {
                    lblStatus.Text = "⚠️ Please enter valid consumer number and amount.";
                    lblStatus.ForeColor = System.Drawing.Color.OrangeRed;
                    return;
                }

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    string query = @"INSERT INTO Payments(user_id, consumer_number, amount, method, status, txn_ref)
                                     VALUES(@u, @c, @a, @m, 'Success', @r);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@u", uid);
                        cmd.Parameters.AddWithValue("@c", consumerNumber);
                        cmd.Parameters.AddWithValue("@a", amount);
                        cmd.Parameters.AddWithValue("@m", method);
                        cmd.Parameters.AddWithValue("@r", Guid.NewGuid().ToString().Substring(0, 10));

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblStatus.Text = "🎉 Payment recorded successfully!";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                btnSubmit.Enabled = false;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ Error " ;
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
