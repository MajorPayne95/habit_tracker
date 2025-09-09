using System;
using Microsoft.Data.Sqlite;

namespace sql_management
{
    public class SQLGenerator
    {
        public static void GenerateSQLTable(string connectionString, string tableName)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $@"CREATE TABLE IF NOT EXISTS [{tableName}] (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Data TEXT,
                        Quantity INTEGER
                        );";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void GenerateHabitTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS habits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Type TEXT NOT NULL,
                        TableName TEXT NOT NULL UNIQUE
                        );";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}