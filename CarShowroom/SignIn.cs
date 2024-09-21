using DatabaseProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarShowroom
{
    public partial class SignIn : Form
    {
        DBAccess objBDAccess = new DBAccess();
        DataTable dtUsers = new DataTable();
        public SignIn()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your username");
            }
            else if (!IsValidEmail(username))
            {
                MessageBox.Show("Please enter a valid email address for your username");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password");
            }
            else if (!HasRequiredPasswordCharacters(password))
            {
                MessageBox.Show("Please enter a password with at least one lowercase letter, one uppercase letter, and one digit");
            }
            else
            {
                string query = "Select * from Users Where email = '" + username + "' AND password = '" +password + "'";
                objBDAccess.readDatathroughAdapter(query,dtUsers);

                if(dtUsers.Rows.Count >= 1)
                {
                    MessageBox.Show("Successfully Loggin");
                    objBDAccess.closeConn();

                    this.Hide();

                    Dashboard home = new Dashboard();
                    home.Show();
                }
                else
                {
                    MessageBox.Show("Your UserName or password is incorrect");
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp(); 
            signUp.Show();
        }
        // Helper method to check if the entered username is a valid email
        private bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        // Helper method to check password requirements
        private bool HasRequiredPasswordCharacters(string password)
        {
            return password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }
    }
}
