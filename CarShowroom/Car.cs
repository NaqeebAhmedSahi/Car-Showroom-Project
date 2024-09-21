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
    public partial class Car : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

        public Car()
        {
            LoadCarData();
            InitializeComponent();
            // Initialize flowLayoutPanel2
            flowLayoutPanel2 = new FlowLayoutPanel();
            // Set other properties if needed
            // ...

            // Add flowLayoutPanel2 to the Controls collection of the form
            this.Controls.Add(flowLayoutPanel2);

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();

        }
        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Company company = new Company();
            company.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            this.Show();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void LoadCarData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT CarName, CarPrice, CarDescription, ImageName FROM tbl_cars WHERE Active = 1";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //flowLayoutPanel4.Margin = new Padding(10); // Adjust the padding as needed

                        while (reader.Read())
                        {
                            string carName = reader.GetString(0);
                            decimal carPrice = reader.GetDecimal(1);
                            string carDescription = reader.GetString(2);
                            string imageName = reader.GetString(3);

                            // Create a PictureBox for the car image
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
                                Margin = new Padding(5, 5, 5, 5), // Adjust margins
                                BackColor = Color.LightGray,
                                BorderStyle = BorderStyle.FixedSingle // Add border for better visibility
                            };

                            // Create Labels for name, price, and description
                            Label nameLabel = new Label { Text = carName, AutoSize = true, Location = new Point(5, 110) };
                            Label priceLabel = new Label { Text = $"Price: {carPrice:C}", AutoSize = true, Location = new Point(5, 130) };
                            Label descriptionLabel = new Label { Text = carDescription, AutoSize = true, MaximumSize = new Size(160, 0), Location = new Point(5, 150) };

                            // Add controls to the carPanel
                            carPanel.Controls.Add(pictureBox);
                            carPanel.Controls.Add(nameLabel);
                            carPanel.Controls.Add(priceLabel);
                            carPanel.Controls.Add(descriptionLabel);

                            // Handle click event if needed
                            carPanel.Click += CarPanel_Click;

                            // Add the carPanel to the FlowLayoutPanel
                            flowLayoutPanel2.Controls.Add(carPanel);
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
            // Handle Panel click event if needed
            MessageBox.Show($"Clicked on car: {((Panel)sender).Controls.OfType<Label>().First().Text}");
        }
    }
}
