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
    public partial class addCompany : Form
    {
        public addCompany()
        {
            InitializeComponent();
        }

        string imageName;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|All Files (*.*)|*.*";
            openFileDialog.Title = "Select an Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected image file path
                string imagePath = openFileDialog.FileName;

                // Display the selected image in the PictureBox
                pictureBox1.Image = Image.FromFile(imagePath);

                // Define imageName as the file name of the selected image
                imageName = Path.GetFileName(imagePath);

                // Optionally, store the image path for further use (e.g., saving it to the database)
                // string storedImagePath = imagePath;

                // Now, you can use 'imageName' in your InsertIntoDatabase method or elsewhere
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            // Toggle the checked state of radioButton1
            radioButton1.Checked = !radioButton1.Checked;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            // Toggle the checked state of radioButton2
            radioButton2.Checked = !radioButton2.Checked;
        }

        private void addCompany_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
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

            //MessageBox.Show(pictureBox1.Text);
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

            // Check if "NotSpecified" values are set before inserting into the database
            if (feature != 2 && active != 2)
            {
                // Perform the database insertion using the selected values
                InsertIntoDatabase(textBox1.Text, imageName, feature, active);

                // Optionally, show a success message or perform other actions
              
            }
            else
            {
                MessageBox.Show("Please select values for both Feature and Active.");
            }
            ManageCompany manageCompany = new ManageCompany();
            manageCompany.Show();
            this.Hide();

        }
        private void InsertIntoDatabase(string title, string imageName, int feature, int active)
        {
            try
            {
                // Validate that imageName is not null or empty
                if (string.IsNullOrWhiteSpace(imageName))
                {
                    MessageBox.Show("Image Name cannot be empty.");
                    return;
                }

                string connectionString = "Data Source=NaqeebAhmedSahi\\SQLEXPRESS;Initial Catalog=sign_up;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO tbl_company (Title, ImageName, Featured, Active) VALUES (@Title, @ImageName, @Featured, @Active)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@ImageName", imageName);
                        command.Parameters.AddWithValue("@Featured", feature);
                        command.Parameters.AddWithValue("@Active", active);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Data inserted into the database!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

