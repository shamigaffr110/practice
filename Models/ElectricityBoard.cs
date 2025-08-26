using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseConnection;

namespace ElectricityBillProject.Models
{
    public class ElectricityBoard
    {
        public void CalculateBill(ElectricityBill ebill)
        {
            int units = ebill.UnitsConsumed;
            double amount = 0;

            if (units <= 100) { amount = 0; }
            else if (units <= 300) { amount = (units - 100) * 1.5; }
            else if (units <= 600) { amount = (200 * 1.5) + ((units - 300) * 3.5); }
            else if (units <= 1000) { amount = (200 * 1.5) + (300 * 3.5) + ((units - 600) * 5.5); }
            else { amount = (200 * 1.5) + (300 * 3.5) + (400 * 5.5) + ((units - 1000) * 7.5); }

            ebill.BillAmount = amount;
        }

        public void AddBill(ElectricityBill ebill)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string sql = "INSERT INTO ElectricityBill(consumer_number,consumer_name,units_consumed,bill_amount,status) VALUES(@num,@name,@units,@amount,'Unpaid')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@num", ebill.ConsumerNumber);
                cmd.Parameters.AddWithValue("@name", ebill.ConsumerName);
                cmd.Parameters.AddWithValue("@units", ebill.UnitsConsumed);
                cmd.Parameters.AddWithValue("@amount", ebill.BillAmount);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            List<ElectricityBill> list = new List<ElectricityBill>();
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string sql = "SELECT TOP (@n) consumer_number, consumer_name, units_consumed, bill_amount FROM ElectricityBill ORDER BY bill_date DESC";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@n", num);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ElectricityBill eb = new ElectricityBill
                    {
                        ConsumerNumber = dr["consumer_number"].ToString(),
                        ConsumerName = dr["consumer_name"].ToString(),
                        UnitsConsumed = Convert.ToInt32(dr["units_consumed"]),
                        BillAmount = Convert.ToDouble(dr["bill_amount"])
                    };
                    list.Add(eb);
                }
                con.Close();
            }
            return list;
        }

        public bool IsValidConsumerNumber(string num)
        {
            if (string.IsNullOrEmpty(num)) return false;
            if (!num.StartsWith("EB")) return false;
            if (num.Length != 7) return false;

            int digits;
            return int.TryParse(num.Substring(2), out digits);
        }
    }
}
