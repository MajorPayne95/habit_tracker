using System;
using Microsoft.Data.Sqlite;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLUpdate
    {
        public static void UpdateRecord(string connectionString)
        {
            Console.Clear();
            SQLRead.ViewAllRecords(connectionString);
            
            MenuManager.UpdateMenu();
            var recordId = Convert.ToInt32(InputManager.GetUserInput());

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkDmd = connection.CreateCommand();
                checkDmd.CommandText =
                    $"SELECT COUNT(*) FROM drinking_water WHERE Id = {recordId};";
                int checkQuery = Convert.ToInt32(checkDmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"No record found with the ID {recordId}.\n\n");
                    connection.Close();
                    UpdateRecord(connectionString);
                }

                MenuManager.DateMenu();
                string date = InputManager.GetDateInput();

                MenuManager.WaterMenu();
                int quantity = Convert.ToInt32(InputManager.GetUserInput());

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"UPDATE drinking_water SET Data = '{date}', Quantity = {quantity} WHERE Id = {recordId};";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }  
}