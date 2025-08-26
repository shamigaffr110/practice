using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class BillReceipt : System.Web.UI.Page
    {
        protected int paymentId = 0;
        protected string htmlContent = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["paymentId"], out paymentId) || paymentId <= 0)
            {
                litReceipt.Text = "<div class='alert alert-danger'>Invalid payment ID.</div>";
                btnDownloadPdf.Visible = false;
                return;
            }

            LoadReceiptDetails(paymentId);
        }

        private void LoadReceiptDetails(int paymentId)
        {
            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(@"
                        SELECT p.payment_id, p.consumer_number, p.amount, p.payment_date, p.method, p.status, p.txn_ref,
                               c.name as user_name, c.phone, c.email, c.address,
                               e.consumer_name, e.units_consumed, e.bill_amount, e.bill_date
                        FROM Payments p
                        LEFT JOIN Connections c ON c.consumer_number = p.consumer_number
                        LEFT JOIN ElectricityBill e ON e.consumer_number = p.consumer_number
                        WHERE p.payment_id = @pid", con);

                    cmd.Parameters.AddWithValue("@pid", paymentId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string consumer = dr["consumer_number"].ToString();
                            string cname = dr["consumer_name"].ToString();
                            string userName = dr["user_name"].ToString();
                            string phone = dr["phone"].ToString();
                            string email = dr["email"].ToString();
                            string address = dr["address"].ToString();
                            int units = Convert.ToInt32(dr["units_consumed"]);
                            double billAmount = Convert.ToDouble(dr["bill_amount"]);
                            DateTime billDate = Convert.ToDateTime(dr["bill_date"]);
                            double amount = Convert.ToDouble(dr["amount"]);
                            DateTime paymentDate = Convert.ToDateTime(dr["payment_date"]);
                            string method = dr["method"].ToString();
                            string status = dr["status"].ToString();
                            string txnRef = dr["txn_ref"].ToString();

                            htmlContent = $@"
                                <div>
                                    <h5>JBVNL Electricity Bill Payment Receipt</h5>
                                    <hr />
                                    <p><strong>Consumer Number:</strong> {consumer}</p>
                                    <p><strong>Consumer Name:</strong> {cname}</p>
                                    <p><strong>Customer Name:</strong> {userName}</p>
                                    <p><strong>Phone:</strong> {phone}</p>
                                    <p><strong>Email:</strong> {email}</p>
                                    <p><strong>Address:</strong> {address}</p>
                                    <hr />
                                    <p><strong>Bill Date:</strong> {billDate:yyyy-MM-dd}</p>
                                    <p><strong>Units Consumed:</strong> {units} kWh</p>
                                    <p><strong>Bill Amount:</strong> Rs. {billAmount:F2}</p>
                                    <hr />
                                    <p><strong>Payment Amount:</strong> Rs. {amount:F2}</p>
                                    <p><strong>Payment Date:</strong> {paymentDate:yyyy-MM-dd HH:mm}</p>
                                    <p><strong>Payment Method:</strong> {method}</p>
                                    <p><strong>Status:</strong> {status}</p>
                                    <p><strong>Transaction Reference:</strong> {txnRef}</p>
                                    <hr />
                                    <p>Thank you for your payment.</p>
                                </div>";

                            litReceipt.Text = htmlContent;
                            btnDownloadPdf.Visible = true;
                        }
                        else
                        {
                            litReceipt.Text = "<div class='alert alert-warning'>Payment details not found.</div>";
                            btnDownloadPdf.Visible = false;
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                litReceipt.Text = $"<div class='alert alert-danger'>Error loading receipt</div>";
                btnDownloadPdf.Visible = false;
            }
        }

        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            GeneratePdf(paymentId);
        }

        private void GeneratePdf(int paymentId)
        {
            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(@"
                        SELECT p.payment_id, p.consumer_number, p.amount, p.payment_date, p.method, p.status, p.txn_ref,
                               c.name as user_name, c.phone, c.email, c.address,
                               e.consumer_name, e.units_consumed, e.bill_amount, e.bill_date
                        FROM Payments p
                        LEFT JOIN Connections c ON c.consumer_number = p.consumer_number
                        LEFT JOIN ElectricityBill e ON e.consumer_number = p.consumer_number
                        WHERE p.payment_id = @pid", con);

                    cmd.Parameters.AddWithValue("@pid", paymentId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            Response.Write("<script>alert('Payment details not found.');</script>");
                            return;
                        }

                        // PDF setup
                        Document doc = new Document(PageSize.A4, 36, 36, 54, 54);
                        MemoryStream ms = new MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                        // Add header/footer event
                        writer.PageEvent = new PdfPageEvents();

                        doc.Open();

                        // Add Logo - update path as per your project structure
                        string logoPath = Server.MapPath("~/images/logo.png");
                        if (System.IO.File.Exists(logoPath))
                        {
                            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                            logo.ScaleToFit(120f, 80f);
                            logo.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                            doc.Add(logo);
                        }

                        // Title
                        Paragraph title = new Paragraph("JBVNL Electricity Bill Payment Receipt", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f;
                        doc.Add(title);

                        // Details table
                        PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
                        table.SetWidths(new float[] { 40, 60 });

                        void AddCell(string label, string val)
                        {
                            PdfPCell c1 = new PdfPCell(new Phrase(label)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5 };
                            PdfPCell c2 = new PdfPCell(new Phrase(val)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5 };
                            table.AddCell(c1);
                            table.AddCell(c2);
                        }

                        AddCell("Consumer Number:", dr["consumer_number"].ToString());
                        AddCell("Consumer Name:", dr["consumer_name"].ToString());
                        AddCell("Customer Name:", dr["user_name"].ToString());
                        AddCell("Phone:", dr["phone"].ToString());
                        AddCell("Email:", dr["email"].ToString());
                        AddCell("Address:", dr["address"].ToString());
                        AddCell("Bill Date:", Convert.ToDateTime(dr["bill_date"]).ToString("yyyy-MM-dd"));
                        AddCell("Units Consumed (kWh):", dr["units_consumed"].ToString());
                        AddCell("Bill Amount (Rs.):", Convert.ToDouble(dr["bill_amount"]).ToString("F2"));
                        AddCell("Payment Amount (Rs.):", Convert.ToDouble(dr["amount"]).ToString("F2"));
                        AddCell("Payment Date:", Convert.ToDateTime(dr["payment_date"]).ToString("yyyy-MM-dd HH:mm"));
                        AddCell("Payment Method:", dr["method"].ToString());
                        AddCell("Payment Status:", dr["status"].ToString());
                        AddCell("Transaction Ref:", dr["txn_ref"].ToString());

                        doc.Add(table);

                        // Footer message
                        Paragraph footer = new Paragraph("\nThank you for your payment.\nJBVNL Utility Services", FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12));
                        footer.Alignment = Element.ALIGN_CENTER;
                        doc.Add(footer);

                        doc.Close();

                        byte[] bytes = ms.ToArray();

                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", $"attachment;filename=Receipt_{paymentId}.pdf");
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.Flush();
                        Response.End();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error generating PDF " + "');</script>");
            }
        }
    }

    // Header/Footer Event Handler
    public class PdfPageEvents : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footerTbl = new PdfPTable(1) { TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin };
            footerTbl.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Phrase("© 2025 JBVNL Electricity Board - All rights reserved.", FontFactory.GetFont(FontFactory.HELVETICA, 9)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            footerTbl.AddCell(cell);

            footerTbl.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin - 10, writer.DirectContent);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfPTable headerTbl = new PdfPTable(1) { TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin };
            headerTbl.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Phrase("JBVNL Electricity Board", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.PaddingBottom = 10;
            headerTbl.AddCell(cell);

            headerTbl.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
        }
    }
}
