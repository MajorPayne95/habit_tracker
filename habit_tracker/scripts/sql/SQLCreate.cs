using System;
using System.Globalization;
using Microsoft.Data.Sqlite;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLCreate
    {
        public static void CreateRecord(string connectionString)
        {
            string date = Program.GetDateInput();
            MenuManager.WaterMenu();
            int quantity = Program.GetNumberInput();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO drinking_water(Data, Quantity) VALUES ('{date}', {quantity});";
                tableCmd.ExecuteNonQuery();
                Console.WriteLine("Record inserted successfully.\n");
            }
        }
    }  
}