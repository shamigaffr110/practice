using System;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.CssClass = "";

            string name = txtName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string dobText = txtDOB.Text.Trim();
            string password = txtPassword.Text.Trim();

            DateTime dob;
            if (!DateTime.TryParse(dobText, out dob))
            {
                dob = DateTime.MinValue; 
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Please fill all required fields.";
                return;
            }

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"INSERT INTO Users(name, phone, email, dob, password) 
                                     VALUES(@name, @phone, @email, @dob, @password)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);  

                    if (dob == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@dob", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@dob", dob);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    lblMsg.CssClass = "text-success";
                    lblMsg.Text = "Registration successful! You can now login.";

                    // Clear inputs
                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                    txtDOB.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation (email)
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Email already registered.";
                }
                else
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Database error:  Error Occured";
                }
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error ! ";
            }
        }
    }
}
