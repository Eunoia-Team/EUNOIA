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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        DatabaseAccess access;
        private void btnSignup_Click(object sender, EventArgs e)
        {

            if (tbUsername2.Text != string.Empty
                && tbEmail.Text != string.Empty
                && tbPassword2.Text != string.Empty
                && tbConfirmPass.Text != string.Empty)
            {
                if (tbPassword2.Text == tbConfirmPass.Text)
                {
                    checkAccount(tbUsername2.Text);
                }
                else
                {
                    MessageBox.Show("Password are not the same", "Error");
                }
            }
        }

        private void checkAccount(string username)
        {
            access = new DatabaseAccess();
            access.createDatabase();
            access.getConnection();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(access.connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    conn.Open();
                    int count = 0;
                    string query = @"SELECT * FROM Account WHERE Username = '" + username + "'";
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("You had created an account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (count == 0)
                    {
                        insertData(tbUsername2.Text, tbEmail.Text, tbPassword2.Text);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void insertData(string username, string email, string password)
        {
            access = new DatabaseAccess();
            access.getConnection();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(access.connectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"INSERT INTO Account(Username, Email, Password) VALUES(@username, @email, @password)";
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    cmd.Parameters.Add(new SQLiteParameter("@username", username));
                    cmd.Parameters.Add(new SQLiteParameter("@email", email));
                    cmd.Parameters.Add(new SQLiteParameter("@password", password));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("An Account has been created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lblExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbUsername2_Click(object sender, EventArgs e)
        {
            tbUsername2.Clear();
        }

        private void tbEmail_Click(object sender, EventArgs e)
        {
            tbEmail.Clear();
        }

        private void tbPassword2_Click(object sender, EventArgs e)
        {
            tbPassword2.Clear();
        }

        private void tbConfirmPass_Click(object sender, EventArgs e)
        {
            tbConfirmPass.Clear();
        }

       
    }
}
