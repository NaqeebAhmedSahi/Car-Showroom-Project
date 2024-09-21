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
    public partial class HomePage : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";
        int id;
        public HomePage()
        {
            InitializeComponent();
            LoadCompanyImages();
            LoadCarData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            this.Hide();
            this.Show();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Company company = new Company();
            company.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Prac prac = new Prac(1000); 
            prac.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           //Dashboard dashboard = new Dashboard();
           //dashboard.Show();
           //this.Hide();
           SignIn signIn = new SignIn(); 
            signIn.Show();
            this.Hide();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
        private void LoadCompanyImages()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ImageName FROM tbl_company WHERE Active = 1";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string imageName = reader.GetString(0);

                            // Construct the absolute path to the image
                            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompanyImages", imageName);

                            PictureBox pictureBox = new PictureBox
                            {
                                Image = Image.FromFile(imagePath),
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Size = new Size(243, 131),
                                Tag = imageName,
                                Margin = new Padding(20, 3, 20, 3)
                            };

                            pictureBox.Click += PictureBox_Click;

                            flowLayoutPanel2.Controls.Add(pictureBox);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading company images: {ex.Message}");
            }
        }


        private void OpenCarPage(int companyId)
        {
            try
            {
                // Create an instance of the CarPage or navigate to the existing one
                Prac carPage = new Prac(companyId);

                // Show the car page
                carPage.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening car page: {ex.Message}");
            }
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                string imageName = ((PictureBox)sender).Tag as string;

                if (imageName != null)
                {
                    int companyId = GetCompanyIdFromImageName(imageName);

                    if (companyId != -1) // Assuming -1 indicates an error or not found
                    {
                        OpenCarPage(companyId);
                    }
                    else
                    {
                        MessageBox.Show($"Company ID not found for image: {imageName}");
                    }
                }
                else
                {
                    MessageBox.Show($"Tag is null or not in the correct format.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling PictureBox click: {ex.Message}");
            }
        }

        private int GetCompanyIdFromImageName(string imageName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id FROM [sign_up].[dbo].[tbl_company] WHERE ImageName = @ImageName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageName", imageName);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int companyId))
                        {
                            return companyId;
                        }
                        else
                        {
                            return -1; // Indicates an error or not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting Company ID from ImageName: {ex.Message}");
                return -1; // Indicates an error
            }
        }
        private void LoadCarData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT CarId, CarName, CarPrice, CarDescription, ImageName FROM tbl_cars WHERE Active = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0); // Assuming id is an integer
                            string carName = reader.GetString(1);
                            decimal carPrice = reader.GetDecimal(2);
                            string carDescription = reader.GetString(3);
                            string imageName = reader.GetString(4);


                            PictureBox pictureBox = new PictureBox
                            {
                                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CarImages", imageName)),
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Size = new Size(175, 105),
                                Tag = imageName,
                                Margin = new Padding(3, 3, 3, 3)
                            };

                            // Create a Panel for each car
                            Panel carPanel = new Panel
                            {
                                Size = new Size(175, 180), // Set the size as per your requirement
                                Margin = new Padding(0), // Set margins
                                BackColor = Color.LightGray,
                                Tag = id
                            };

                            // Create Labels for name, price, and description
                            Label nameLabel = new Label { Text = carName, AutoSize = true, Location = new Point(0, 105) };
                            Label priceLabel = new Label { Text = $"Price: {carPrice:C}", AutoSize = true, Location = new Point(0, 125) };
                            Label descriptionLabel = new Label { Text = carDescription, AutoSize = true, MaximumSize = new Size(130, 0), Location = new Point(0, 145) };

                            // Add controls to the carPanel
                            carPanel.Controls.Add(pictureBox);
                            carPanel.Controls.Add(nameLabel);
                            carPanel.Controls.Add(priceLabel);
                            carPanel.Controls.Add(descriptionLabel);

                            // Handle click event if needed
                            carPanel.Click += CarPanel_Click;

                            // Add the carPanel to the FlowLayoutPanel
                            flowLayoutPanel4.Controls.Add(carPanel);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading car data: {ex.Message}");
            }
        }

        private void CarPanel_Click(object sender, EventArgs e)
        {
            try
            {
                // Extract the car ID from the Tag property of the clicked panel
                int clickedCarId = (int)((Panel)sender).Tag;

                // Open the order form and pass the car ID
                order order = new order(clickedCarId);
                order.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening order form: {ex.Message}");
            }
        }



        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
