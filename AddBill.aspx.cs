using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using ElectricityBillProject.Models;  
using DatabaseConnection;             

namespace ElectricityBillProject
{
    public partial class AddBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        ElectricityBoard eb = new ElectricityBoard();

        protected void btnGen_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.CssClass = "";

            try
            {
                string consumerNumber = txtConsumer.Text.Trim();

                // Validate Consumer Number - throws FormatException if invalid
                ValidateConsumerNumber(consumerNumber);

                ElectricityBill bill = new ElectricityBill
                {
                    ConsumerNumber = consumerNumber,
                    ConsumerName = txtName.Text.Trim()
                };

                int unitsConsumed;
                if (!int.TryParse(txtUnits.Text.Trim(), out unitsConsumed))
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Units must be a valid number.";
                    return;
                }

                // BillValidator  for validation
                var validator = new BillValidator();
                string validationMsg = validator.ValidateUnitsConsumed(unitsConsumed);
                if (validationMsg != "OK")
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = validationMsg;
                    return;
                }

                bill.UnitsConsumed = unitsConsumed;

                // Calculate bill amount 
                eb.CalculateBill(bill);

                // Add bill to DB
                eb.AddBill(bill);

                lblMsg.CssClass = "text-success";
                lblMsg.Text = $"Bill added successfully. Amount: ₹{bill.BillAmount:F2}";
            }
            catch (FormatException fex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Oops Error"; 
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error  " ;
            }
        }

        protected void btnFetch_Click(object sender, EventArgs e)
        {
            int n = 5;
            int.TryParse(txtN.Text.Trim(), out n);
            var list = eb.Generate_N_BillDetails(n);
            grid.DataSource = list;
            grid.DataBind();
        }

        // Validation method for Consumer Number
        private void ValidateConsumerNumber(string consumerNumber)
        {
            if (string.IsNullOrEmpty(consumerNumber) || !Regex.IsMatch(consumerNumber, @"^EB\d{5}$"))
            {
                throw new FormatException("Invalid Consumer Number");
            }
        }
    }
}
