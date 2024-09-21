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
    public partial class Company : Form
    {
        string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";


        public Company()
        {
            InitializeComponent();
            LoadCompanyImages();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Company_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            this.Hide();
            this.Show();
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Prac prac = new Prac(1000);
            prac.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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



        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
