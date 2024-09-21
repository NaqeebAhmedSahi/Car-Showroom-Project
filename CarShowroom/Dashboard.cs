using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarShowroom
{
    public partial class Dashboard : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        public Dashboard()
        {
            InitializeComponent();
            UpdateDashboard();
        }

        private void UpdateDashboard()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to get the total count of companies
                    string companyCountQuery = "SELECT COUNT(*) FROM tbl_company WHERE Active = 1";
                    int totalCompanies = ExecuteScalar<int>(companyCountQuery, connection);

                    // Query to get the total count of cars
                    string carCountQuery = "SELECT COUNT(*) FROM tbl_cars WHERE Active = 1";
                    int totalCars = ExecuteScalar<int>(carCountQuery, connection);

                    // Query to get the total count of orders
                    string orderCountQuery = "SELECT COUNT(*) FROM tbl_order";
                    int totalOrders = ExecuteScalar<int>(orderCountQuery, connection);

                    // Query to get the total price of orders
                    string totalPriceQuery = "SELECT SUM(Total) FROM tbl_order";
                    decimal totalPrice = ExecuteScalar<decimal>(totalPriceQuery, connection);

                    // Update the labels in your dashboard
                    label5.Text = $"Total Companies: \n{totalCompanies}";
                    label6.Text = $"Total Cars: \n{totalCars}";
                    label7.Text = $"Total Orders: \n{totalOrders}";
                    label8.Text = $"Total Price: \n{totalPrice:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating dashboard: {ex.Message}");
            }
        }

        private T ExecuteScalar<T>(string query, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return (T)Convert.ChangeType(result, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage(); 
            homePage.Show();
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageCompany manageCompany = new ManageCompany();
            manageCompany.Show();
            this.Hide();

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageCar manageCar = new ManageCar();
            manageCar.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
