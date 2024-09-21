using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CarShowroom
{
    public partial class order : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        int carId;
        string carName;
        decimal carPrice;
        public order(int id)
        {
            InitializeComponent();
            carId = id;

            LoadCarDetails();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void LoadCarDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT CarName, CarPrice, CarDescription, ImageName FROM tbl_cars WHERE CarID = @CarID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CarID", carId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the form controls with car details
                                label1.Text = reader.GetString(0);
                                carName = reader.GetString(0);
                                label2.Text = $"Price: {reader.GetDecimal(1):C}";
                                carPrice = reader.GetDecimal(1);
                                label3.Text = reader.GetString(2);

                                // Load the car image into the PictureBox
                                pictureBox1.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CarImages", reader.GetString(3)));
                            }
                            else
                            {
                                MessageBox.Show("Car details not found.");
                                // Handle the case when no car details are found (e.g., display an error message).
                                // You may choose to close the form or take appropriate action based on your application's requirements.
                                Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading car details: {ex.Message}");
                // Handle the exception as needed (e.g., display an error message).
                // You may choose to close the form or take appropriate action based on your application's requirements.
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get user-entered information
                string customerName = textBox2.Text;
                string customerPhone = textBox3.Text;
                string customerAddress = textBox4.Text;
                int quantity = int.Parse(textBox1.Text);

                // Calculate total
                decimal total = quantity * carPrice;

        // Insert order data into the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO [tbl_order] (Car, Price, Quantity, Total, CustomerName, CustomerPhone, CustomerAddress) " +
                                   "VALUES (@Car, @Price, @Quantity, @Total, @CustomerName, @CustomerPhone, @CustomerAddress)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Car", carId);
                        command.Parameters.AddWithValue("@Price", carPrice);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Total", total);
                        command.Parameters.AddWithValue("@CustomerName", customerName);
                        command.Parameters.AddWithValue("@CustomerPhone", customerPhone);
                        command.Parameters.AddWithValue("@CustomerAddress", customerAddress);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Order submitted successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting order: {ex.Message}");
            }
        }
    }
}

