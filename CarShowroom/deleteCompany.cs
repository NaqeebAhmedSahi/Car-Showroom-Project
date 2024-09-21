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
    public partial class deleteCompany : Form
    {
        private string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        public deleteCompany()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string companyNameToDelete = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(companyNameToDelete))
            {
                // Perform the deletion in the database
                DeleteCompanyFromDatabase(companyNameToDelete);

                // Optionally, perform any other actions after deletion

                MessageBox.Show($"Company '{companyNameToDelete}' deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please enter the company name to delete.");
            }
            ManageCompany manageCompany = new ManageCompany();
            manageCompany.Show();
            this.Hide();
        }
        private void DeleteCompanyFromDatabase(string companyName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Perform the deletion based on the company name
                    string deleteQuery = $"DELETE FROM tbl_company WHERE Title = '{companyName}'";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}

