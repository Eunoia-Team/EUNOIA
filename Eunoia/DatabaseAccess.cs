using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Eunoia
{
    class DatabaseAccess
    {
        public string connectionString { get; set; }
        string connection;
        public void getConnection()
        {
            connection = @"Data Source = EunoiaDB.db; Version=3";
            connectionString = connection;
        }

        public void createDatabase()
        {
            if (!File.Exists("EunoiaDB.db"))
            {
                try
                {
                    File.Create("EunoiaDB.db");
                    createAccountTable();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                createAccountTable();
            }
        }

        private void createAccountTable()
        {
            try
            {
                getConnection();
                using (SQLiteConnection conn = new SQLiteConnection(connection))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"CREATE TABLE Account(Id INTEGER PRIMARY KEY AUTOINCREMENT, Username Text(20), Email Text(25), Password Text(25))";
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
