using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatabaseProject;
using System.Text.RegularExpressions;

namespace CarShowroom
{
    public partial class SignUp : Form
    {
        DBAccess objDBAccess = new DBAccess();
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string name = textBox3.Text.Trim();
            string country = comboBox1.Text.Trim();

            if (String.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your username");
            }
            else
            {
                // Check if the entered username is a valid email
                try
                {
                    var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
                    if (!regex.IsMatch(username))
                    {
                        MessageBox.Show("Please enter a valid email address for your username");
                        return; // Stop further processing if email is not valid
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    MessageBox.Show("An error occurred while validating the email address");
                    return; // Stop further processing if an error occurs during validation
                }

                // Continue processing for other fields
                if (String.IsNullOrWhiteSpace(password) || !HasRequiredPasswordCharacters(password))
                {
                    MessageBox.Show("Please enter a password with at least one lowercase letter, one uppercase letter, and one digit");
                    return; // Stop further processing if password is not valid
                }
                else if (String.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter your name");
                }
                else if (String.IsNullOrWhiteSpace(country))
                {
                    MessageBox.Show("Please select your country");
                }
            
            else
            {
                SqlCommand insertCommand = new SqlCommand("insert into Users(name,email,password,country) values(@name,@username,@password,@country)");
                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@username", username);
                insertCommand.Parameters.AddWithValue("@password", password);
                insertCommand.Parameters.AddWithValue("@country", country);
                int row = objDBAccess.executeQuery(insertCommand);
                if (row == 1)
                {
                    MessageBox.Show("Sign Up Successfully");

                    this.Hide();
                    SignIn signin = new SignIn();
                    signin.Show();
                }
                else
                {
                    MessageBox.Show("Error Occured");
                }
                }
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignIn signin = new SignIn();
            signin.Show();  
        }
        // Helper method to check password requirements
        private bool HasRequiredPasswordCharacters(string password)
        {
            return password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }
    }
}
