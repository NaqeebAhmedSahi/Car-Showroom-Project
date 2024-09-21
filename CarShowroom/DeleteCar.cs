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
    public partial class DeleteCar : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        public DeleteCar()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string carNameToDelete = textBox1.Text;

            if (!string.IsNullOrEmpty(carNameToDelete))
            {
                // Perform the deletion
                DeleteCarFromDatabase(carNameToDelete);
            }
            else
            {
                MessageBox.Show("Please enter a car name to delete.");
            }
            ManageCar manageCar = new ManageCar();
            manageCar.Show();
            this.Hide();
        }
        private void DeleteCarFromDatabase(string carNameToDelete)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM tbl_cars WHERE CarName = @CarName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CarName", carNameToDelete);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Car '{carNameToDelete}' deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show($"Car '{carNameToDelete}' not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting car: {ex.Message}");
            }

        }
    }
}

