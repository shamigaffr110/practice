using System;
using System.Data;
using System.Data.SqlClient;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class CustomerSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: load recent or all customers on first load
            if (!IsPostBack)
            {
                LoadCustomers("");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            LoadCustomers(keyword);
        }

        private void LoadCustomers(string keyword)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = @"
                    SELECT connection_id, name, phone, email, address, locality, type, connection_type, status 
                    FROM Connections 
                    WHERE (name LIKE @kw OR phone LIKE @kw OR email LIKE @kw)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                DataTable dt = new DataTable();
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);
                }
                con.Close();

                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
        }
    }
}
