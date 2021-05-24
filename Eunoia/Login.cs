using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Eunoia
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public string usernames;
        DatabaseAccess access;
       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text != String.Empty && tbPassword.Text != string.Empty)
            {
                checkAccount(tbUsername.Text, tbPassword.Text);
            }
        }

        private void checkAccount(string username, string password)
        {
            access = new DatabaseAccess();
            access.getConnection();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(access.connectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"SELECT * FROM Account WHERE Username ='" + username + "' and Password = '" + password + "'";
                    int count = 0;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    SQLiteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("Logged in", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        usernames = username;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbUsername_Click(object sender, EventArgs e)
        {
            tbUsername.Clear();
        }

        private void tbPassword_Click(object sender, EventArgs e)
        {
            tbPassword.Clear();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            signup.ShowDialog();
        }
    }
}
