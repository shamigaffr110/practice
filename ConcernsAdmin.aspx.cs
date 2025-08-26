using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class ConcernsAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadConcerns();
            }
        }

        private void LoadConcerns()
        {
            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT concern_id, user_id, consumer_number, message, status, created_at, resolved_at
                        FROM Concerns
                        ORDER BY created_at DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gridConcerns.DataSource = dt;
                        gridConcerns.DataBind();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error loading concerns " ;
            }
        }

        protected void gridConcerns_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resolve")
            {
                try
                {
                    int concernId = Convert.ToInt32(e.CommandArgument);

                    using (SqlConnection con = DBHandler.GetConnection())
                    {
                        con.Open();

                        string query = "UPDATE Concerns SET status = 'Resolved', resolved_at = GETDATE() WHERE concern_id = @id";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@id", concernId);
                            cmd.ExecuteNonQuery();
                        }

                        con.Close();
                    }

                    lblMsg.Text = $"Concern {concernId} marked as resolved.";
                    LoadConcerns();
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Error resolving concern " ;
                }
            }
        }

        protected void btnResolveAll_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = "UPDATE Concerns SET status = 'Resolved', resolved_at = GETDATE() WHERE status = 'Open'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        lblMsg.Text = $"{rowsAffected} open concern(s) marked as resolved.";
                    }

                    con.Close();
                }

                LoadConcerns();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error resolving concerns " ;
            }
        }
    }
}
