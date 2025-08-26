using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class UserDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblBillMsg.Text = "";
                litMessage.Text = "";
                pnlTransactions.Visible = false;
            }
        }
        
        protected void btnPrintBill_Click(object sender, EventArgs e)
        {
            GeneratePdfBill(inline: true);
        }

        protected void btnDownloadBill_Click(object sender, EventArgs e)
        {
            GeneratePdfBill(inline: false);
        }

        protected void btnEmailBill_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            if (string.IsNullOrEmpty(consumerNumber))
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Please enter consumer number.";
                return;
            }

            string filePath = Server.MapPath($"~/TempBills/Bill_{consumerNumber}.pdf");
            GeneratePdfBill(saveToFile: true, filePath: filePath);

            string userEmail = GetUserEmailByConsumerNumber(consumerNumber);
            if (string.IsNullOrEmpty(userEmail))
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Email not found for this consumer.";
                return;
            }

            try
            {
                var mail = new MailMessage
                {
                    Subject = $"Electricity Bill - {consumerNumber}",
                    Body = "Please find your electricity bill attached.",
                    IsBodyHtml = false
                };
                mail.To.Add(userEmail);
                mail.Attachments.Add(new Attachment(filePath));

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(mail);
                }

                lblBillMsg.CssClass = "text-success";
                lblBillMsg.Text = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Error sending email: " + ex.Message;
            }
        }

        protected void btnViewConnections_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            if (string.IsNullOrEmpty(consumerNumber))
            {
                lblBillMsg.Text = "Please enter consumer number to view details.";
                return;
            }

            using (var con = DBHandler.GetConnection())
            {
                var daBill = new SqlDataAdapter(
                    "SELECT * FROM ElectricityBill WHERE consumer_number = @cnum ORDER BY bill_date DESC", con);
                daBill.SelectCommand.Parameters.AddWithValue("@cnum", consumerNumber);

                var dtBills = new DataTable();
                daBill.Fill(dtBills);

                var daPayments = new SqlDataAdapter(
                    "SELECT * FROM Payments WHERE consumer_number = @cnum ORDER BY payment_date DESC", con);
                daPayments.SelectCommand.Parameters.AddWithValue("@cnum", consumerNumber);

                var dtPayments = new DataTable();
                daPayments.Fill(dtPayments);

                var daConcerns = new SqlDataAdapter(
                    "SELECT * FROM Concerns WHERE consumer_number = @cnum ORDER BY created_at DESC", con);
                daConcerns.SelectCommand.Parameters.AddWithValue("@cnum", consumerNumber);

                var dtConcerns = new DataTable();
                daConcerns.Fill(dtConcerns);

                _bindTransactionData(dtBills, dtPayments, dtConcerns);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDashboard.aspx");
        }

        private void GeneratePdfBill(bool inline = true, bool saveToFile = false, string filePath = "")
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            if (string.IsNullOrEmpty(consumerNumber))
            {
                lblBillMsg.Text = "Consumer number required!";
                return;
            }

            using (var con = DBHandler.GetConnection())
            {
                var cmd = new SqlCommand(
                    "SELECT consumer_number, consumer_name, units_consumed, bill_amount, bill_date, due_date, status " +
                    "FROM ElectricityBill WHERE consumer_number = @cnum", con);
                cmd.Parameters.AddWithValue("@cnum", consumerNumber);

                con.Open();
                var rdr = cmd.ExecuteReader();
                if (!rdr.HasRows)
                {
                    lblBillMsg.Text = "No bills found for this consumer.";
                    return;
                }

                using (var ms = new MemoryStream())
                {
                    var doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    doc.Add(new Paragraph($"Electricity Bill\nConsumer: {consumerNumber}\nDate: {DateTime.Now}\n\n"));

                    while (rdr.Read())
                    {
                        doc.Add(new Paragraph(
                            $"Bill Date: {((DateTime)rdr["bill_date"]).ToShortDateString()}, " +
                            $"Units: {rdr["units_consumed"]}, " +
                            $"Amount: ₹{rdr["bill_amount"]}, " +
                            $"Status: {rdr["status"]}"
                        ));
                    }

                    doc.Close();
                    var bytes = ms.ToArray();

                    if (saveToFile && !string.IsNullOrEmpty(filePath))
                    {
                        File.WriteAllBytes(filePath, bytes);
                        return;
                    }

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition",
                        inline ? $"inline;filename=Bill_{consumerNumber}.pdf" : $"attachment;filename=Bill_{consumerNumber}.pdf");
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.End();
                }
            }
        }

        private string GetUserEmailByConsumerNumber(string consumerNumber)
        {
            using (var con = DBHandler.GetConnection())
            {
                var cmd = new SqlCommand(
                    "SELECT email FROM Connections WHERE consumer_number = @cnum", con);
                cmd.Parameters.AddWithValue("@cnum", consumerNumber);

                con.Open();
                return cmd.ExecuteScalar()?.ToString();
            }
        }
        private void _bindTransactionData(DataTable bills, DataTable payments, DataTable concerns)
        {
            pnlTransactions.Visible = true;

            gvBills.DataSource = bills;
            gvBills.DataBind();

            gvPayments.DataSource = payments;
            gvPayments.DataBind();

            gvConcerns.DataSource = concerns;
            gvConcerns.DataBind();
        }


    }
}
