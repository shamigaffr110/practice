using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class BillReceiptPdf : System.Web.UI.Page
    {
        // PDF Event Handler (Header/Footer)
        public class PdfHeaderFooter : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;
                Rectangle pageSize = document.PageSize;

                // Logo
                string logoPath = HttpContext.Current.Server.MapPath("~/Styles/jbvnl_logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(100f, 40f);
                    logo.SetAbsolutePosition(document.LeftMargin, pageSize.GetTop(document.TopMargin) + 10);
                    cb.AddImage(logo);
                }

                // Title
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER,
                    new Phrase("JBVNL – Electricity Bill Receipt", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)),
                    pageSize.Width / 2, pageSize.GetTop(document.TopMargin) + 25, 0);

                // Footer - Page number
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER,
                    new Phrase("Page " + writer.PageNumber, FontFactory.GetFont(FontFactory.HELVETICA, 10)),
                    pageSize.Width / 2, pageSize.GetBottom(document.BottomMargin) - 10, 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int pid = 0;
            int.TryParse(Request.QueryString["paymentId"], out pid);
            if (pid <= 0)
            {
                Response.Write("❌ Invalid Payment ID.");
                return;
            }

            DataTable dt = new DataTable();
            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();
                string sql = @"
                    SELECT p.payment_id, p.consumer_number, p.amount, p.payment_date, p.method, p.txn_ref,
                           e.consumer_name, e.units_consumed, e.bill_amount, e.bill_date
                    FROM Payments p
                    LEFT JOIN ElectricityBill e ON e.consumer_number = p.consumer_number
                    WHERE p.payment_id = @pid";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@pid", pid);
                    da.Fill(dt);
                }
                con.Close();
            }

            if (dt.Rows.Count == 0)
            {
                Response.Write("❌ Receipt not found.");
                return;
            }

            DataRow r = dt.Rows[0];

            // Prepare PDF response
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 50, 50, 80, 50);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                writer.PageEvent = new PdfHeaderFooter();
                doc.Open();

                Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLUE);

                void AddRow(string label, string value)
                {
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    table.SetWidths(new int[] { 30, 70 });

                    table.AddCell(new PdfPCell(new Phrase(label, labelFont)) { Border = 0 });
                    table.AddCell(new PdfPCell(new Phrase(value, valueFont)) { Border = 0 });

                    doc.Add(table);
                    doc.Add(new Paragraph("\n"));
                }

                // Section: Customer Details
                doc.Add(new Paragraph("Customer Details", sectionFont));
                doc.Add(new Paragraph(" "));
                AddRow("Consumer Number", r["consumer_number"].ToString());
                AddRow("Consumer Name", r["consumer_name"].ToString());

                // Section: Billing Info
                doc.Add(new Paragraph("Electricity Usage", sectionFont));
                doc.Add(new Paragraph(" "));
                AddRow("Units Consumed", r["units_consumed"].ToString() + " kWh");
                AddRow("Bill Amount", "Rs. " + r["bill_amount"]);
                AddRow("Bill Date", Convert.ToDateTime(r["bill_date"]).ToString("yyyy-MM-dd"));

                // Section: Payment Info
                doc.Add(new Paragraph("Payment Details", sectionFont));
                doc.Add(new Paragraph(" "));
                AddRow("Payment ID", r["payment_id"].ToString());
                AddRow("Txn Reference", r["txn_ref"].ToString());
                AddRow("Amount Paid", "Rs. " + r["amount"]);
                AddRow("Payment Method", r["method"].ToString());
                AddRow("Payment Date", Convert.ToDateTime(r["payment_date"]).ToString("yyyy-MM-dd HH:mm"));

                // Thank you message
                doc.Add(new Paragraph("Thank you for your payment!", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, BaseColor.DARK_GRAY)) { SpacingBefore = 20 });

                doc.Close();
                writer.Close();

                byte[] bytes = ms.ToArray();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"inline; filename=Receipt_{pid}.pdf");
                Response.BinaryWrite(bytes);
                Response.End();
            }
        }
    }
}
