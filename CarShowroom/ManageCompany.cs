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
    public partial class ManageCompany : Form
    {
        public ManageCompany()
        {
            InitializeComponent();
            // Call the method to fetch data and bind it to the DataGridView
            dataGridView1.DataSource = GetDataFromDatabase();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Close();
        }
        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageCompany manageCompany = new ManageCompany();
            manageCompany.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addCompany addCompany = new addCompany();
            addCompany.Show();
            this.Hide();
        }
        private string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM tbl_company";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteCompany deleteCompany = new deleteCompany();
            deleteCompany.Show();
            this.Hide();
        }
    }

}

