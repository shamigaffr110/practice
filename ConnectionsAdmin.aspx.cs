using System;
using System.Data;
using System.Data.SqlClient;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class ConnectionsAdmin : System.Web.UI.Page
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
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string sql = "SELECT connection_id, user_id, consumer_number, name, phone, email, address, locality, type, load, connection_type, status FROM Connections ORDER BY created_at DESC";
                SqlCommand cmd = new SqlCommand(sql, con);
                DataTable dt = new DataTable();

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);
                }
                con.Close();

                gvConnections.DataSource = dt;
                gvConnections.DataBind();
            }
        }

        protected void gvConnections_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int connectionId = Convert.ToInt32(e.CommandArgument);
                string newStatus = e.CommandName == "Approve" ? "Active" : "Inactive";

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    string updateSql = "UPDATE Connections SET status = @status WHERE connection_id = @id";
                    SqlCommand cmd = new SqlCommand(updateSql, con);
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", connectionId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                LoadConnections();
            }
        }
    }
}
