using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class ConnectionInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadConnections();
            }
        }

        private void LoadConnections()
        {
            lblMsg.Text = "";

            if (Session["user_id"] == null)
            {
                lblMsg.Text = "❌ Please login to view your connection details.";
                gvConnections.Visible = false;
                return;
            }

            int userId = Convert.ToInt32(Session["user_id"]);

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT connection_id, consumer_number, name, phone, email, address, locality, type, load, connection_type, status, created_at
                        FROM Connections
                        WHERE user_id = @u";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@u", userId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            gvConnections.DataSource = dt;
                            gvConnections.DataBind();

                            gvConnections.Visible = dt.Rows.Count > 0;
                            if (dt.Rows.Count == 0)
                            {
                                lblMsg.Text = " No connection requests found.";
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = " Error loading connections: " ;
            }
        }
    }
}
