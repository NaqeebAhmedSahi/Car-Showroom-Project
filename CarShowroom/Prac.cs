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

    public partial class Prac : Form
    {
        int id;
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        private int companyId;
        public Prac(int companyId)
        {
            InitializeComponent();
            //LoadCompanyImages();
            this.companyId = companyId;

            if (companyId == 1000)
            {
                LoadCarData();
            }
            else
            {
                // Load cars for the selected company
                LoadCarsForCompany();
            }
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage();
            this.Hide();
            homePage.Show();
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

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void Prac_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadCarsForCompany()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT CarID, CarName,  CarPrice,CarDescription, ImageName FROM tbl_cars WHERE CompanyID = @CompanyId AND Active = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyId", companyId);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cars for the selected company: {ex.Message}");
            }
        }

    }
}