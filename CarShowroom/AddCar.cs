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

namespace CarShowroom
{
    public partial class AddCar : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        private string selectedImagePath;
        public AddCar()
        {
            InitializeComponent();
            PopulateCompanyComboBox();
            comboBox1.SelectedIndex = 0;
        }

        private void AddCar_Load(object sender, EventArgs e)
        {

        }
        string imageN;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected image file path
                    selectedImagePath = openFileDialog.FileName;

                    // Display the selected image in the PictureBox
                    pictureBox1.Image = Image.FromFile(selectedImagePath);

                    // Set the ImageName based on the file name
                    imageN = Path.GetFileName(selectedImagePath);
                    // Optionally, you can further process the imageName (e.g., removing spaces or special characters)
                    //textBoxImageName.Text = imageName;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Retrieve values from controls
            string carName = textBox1.Text;
            string carDescription = textBox2.Text;
            decimal carPrice = decimal.Parse(textBox3.Text); // Handle parsing errors
            string imageName = imageN;
            if (carPrice >= 0)
            {
                // Extract CompanyID from the selected item in the ComboBox
                int companyId = ExtractCompanyIdFromComboBox(comboBox1.SelectedItem.ToString());

                int feature;
                int active;

                // Determine the selected values for Feature
                if (radioButton1.Checked)
                {
                    feature = 1;
                }
                else if (radioButton2.Checked)
                {
                    feature = 0;
                }
                else
                {
                    // Handle the case where neither radio button is checked
                    feature = 2;
                }

                //  MessageBox.Show(pictureBox1.Text);
                // Determine the selected values for Active
                if (radioButton6.Checked)
                {
                    active = 1;
                }
                else if (radioButton5.Checked)
                {
                    active = 0;
                }
                else
                {
                    // Handle the case where neither radio button is checked
                    active = 2;
                }

                // Save the data to the database (implement this method)
                SaveCarDataToDatabase(carName, carDescription, carPrice, imageName, companyId, feature, active);
                ManageCar manageCar = new ManageCar();
                manageCar.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Price should not be negative");
            }
        }

        private int ExtractCompanyIdFromComboBox(string selectedValue)
        {
            // Extract CompanyID from the selected item in the ComboBox
            int startIndex = selectedValue.LastIndexOf("(ID: ") + "(ID: ".Length;
            int endIndex = selectedValue.LastIndexOf(")");

            string companyIdString = selectedValue.Substring(startIndex, endIndex - startIndex);
            int companyId = int.Parse(companyIdString);

            return companyId;
        }

        private void SaveCarDataToDatabase(string carName, string carDescription, decimal carPrice, string imageName, int companyId, int featured, int active)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Assuming you have a Cars table
                    string query = "INSERT INTO tbl_cars (CarName, CarDescription, CarPrice, ImageName, CompanyID, Featured, Active) " +
                                   "VALUES (@CarName, @CarDescription, @CarPrice, @ImageName, @CompanyID, @Featured, @Active)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CarName", carName);
                        command.Parameters.AddWithValue("@CarDescription", carDescription);
                        command.Parameters.AddWithValue("@CarPrice", carPrice);
                        command.Parameters.AddWithValue("@ImageName", imageName);
                        command.Parameters.AddWithValue("@CompanyID", companyId);
                        command.Parameters.AddWithValue("@Featured", featured);
                        command.Parameters.AddWithValue("@Active", active);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Car data saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    

    private void PopulateCompanyComboBox()
        {
            // Assuming you have a method to fetch company names and IDs from the database
            // and populate the ComboBox
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Title FROM tbl_company";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assuming CompanyID is an int and CompanyName is a string
                            int companyId = reader.GetInt32(0);
                            string companyName = reader.GetString(1);

                            // Display both CompanyName and CompanyID in ComboBox
                            comboBox1.Items.Add($"{companyName} (ID: {companyId})");
                        }
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