using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Web;

namespace ElectricityBillProject.Models
{
    public class EmailService
    {
        public static string SendMail(string to, string subject, string bodyHtml, string cc = null, string bcc = null, byte[] attachment = null, string attachName = null)
        {
            try
            {
                string host = ConfigurationManager.AppSettings["SmtpHost"];
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                string user = ConfigurationManager.AppSettings["SmtpUser"];
                string pass = ConfigurationManager.AppSettings["SmtpPass"];
                string from = ConfigurationManager.AppSettings["SmtpFrom"];

                using (SmtpClient client = new SmtpClient(host, port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(user, pass);

                    using (MailMessage mm = new MailMessage())
                    {
                        mm.From = new MailAddress(from, "JBVNL Utility Suite");
                        mm.To.Add(new MailAddress(to));

                        if (!string.IsNullOrEmpty(cc)) mm.CC.Add(cc);
                        if (!string.IsNullOrEmpty(bcc)) mm.Bcc.Add(bcc);

                        mm.Subject = subject;
                        mm.IsBodyHtml = true;

                        string html = "<div style='font-family:Arial;border:1px solid #ccc;padding:10px'>" +
                                      "<div style='text-align:center'>" +
                                      "<img src='cid:jbvnlLogo' style='max-height:60px'/>" +
                                      "<h2>JBVNL – Jharkhand Bijli Vitran Nigam Limited</h2>" +
                                      "</div>" +
                                      "<div style='margin:15px'>" + bodyHtml + "</div>" +
                                      "<hr/>" +
                                      "<div style='font-size:12px;text-align:center;color:gray'>" +
                                      "This is a system-generated email from JBVNL Utility Suite. Please do not reply." +
                                      "</div>" +
                                      "</div>";

                        mm.Body = html;

                        AlternateView av = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
                        LinkedResource lr = new LinkedResource(HttpContext.Current.Server.MapPath("~/Styles/jbvnl_logo.jpg"));
                        lr.ContentId = "jbvnlLogo";
                        av.LinkedResources.Add(lr);
                        mm.AlternateViews.Add(av);

                        if (attachment != null && attachment.Length > 0)
                        {
                            MemoryStream ms = new MemoryStream(attachment);
                            mm.Attachments.Add(new Attachment(ms, string.IsNullOrEmpty(attachName) ? "Receipt.pdf" : attachName, "application/pdf"));
                        }

                        client.Send(mm);
                    }
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return " error " ;
            }
        }
    }
}
