using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TestBunifu
{
    internal class DatabaseOperations
    {
        private string dbFileName = "DownloadManager.db";
        private string dbDirectory = AppDomain.CurrentDomain.BaseDirectory; // Change to the bin folder
        private string dbFilePath;
        private string connectionString;

        public DatabaseOperations()
        {
            // Create directory if it doesn't exist (not necessary in this case)
            // No need to create the directory in the bin folder

            dbFilePath = Path.Combine(dbDirectory, dbFileName);
            connectionString = $"Data Source={dbFilePath};Version=3;";

            // Create the Downloads table if it doesn't exist
            CreateDownloadsTable();
        }

        private void CreateDownloadsTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "CREATE TABLE IF NOT EXISTS Downloads (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Size TEXT, Status TEXT, LastTryDate TEXT, Type TEXT)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating Downloads table: " + ex.Message);
            }
        }

        public SQLiteConnection OpenConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
