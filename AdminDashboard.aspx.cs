using System;
using System.Data;
using System.Data.SqlClient;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMetrics();
            }
        }

        private void LoadMetrics()
        {
            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    // Total Users
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Users", con);
                    lblUsers.Text = cmd1.ExecuteScalar().ToString();

                    // Total Revenue
                    SqlCommand cmd2 = new SqlCommand("SELECT ISNULL(SUM(amount), 0) FROM Payments WHERE status = 'Success'", con);
                    double revenue = Convert.ToDouble(cmd2.ExecuteScalar());
                    lblRevenue.Text = "Rs. " + revenue;

                    // Total Payments
                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Payments", con);
                    lblPayments.Text = cmd3.ExecuteScalar().ToString();

                    // Pending Concerns
                    SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM Concerns WHERE status = 'Open'", con);
                    lblConcerns.Text = cmd4.ExecuteScalar().ToString();
                    //


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error loading dashboard ";
            }
        }
            protected void lnkViewConnections_Click(object sender, EventArgs e)
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Connections", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewConnections.DataSource = dt;
                    GridViewConnections.DataBind();
                    GridViewConnections.Visible = true;
                }
            }

        }
    }

