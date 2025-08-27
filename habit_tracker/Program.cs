using System;
using System.Globalization;
using Microsoft.Data.Sqlite;
using menu_manager;
using error_messages;
using sql_management;

namespace habit_tracker
{
    class Program
    {
        static string connectionString = @"Data Source=habit-Tracker.db";

        static void Main(string[] args)
        {
            SQLGenerator sqlGenerator = new SQLGenerator();
            sqlGenerator.GenerateSQL(connectionString);
            GetUserInput();
        }

        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (!closeApp)
            {
                MenuManager menuManager = new MenuManager();
                DisplayError displayError = new DisplayError();
                menuManager.MainMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.WriteLine("Closing application...\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        SQLRead.ViewAllRecords(connectionString);
                        break;
                    case "2":
                        InsertNewRecord();
                        break;
                    case "3":
                        DeleteRecord();
                        break;
                    case "4":
                        UpdateRecord();
                        break;
                    default:
                        displayError.ErrorMessage("invalid input");
                        break;
                }
            }
        }

        private static void InsertNewRecord()
        {
            string date = GetDateInput();

            int quantity = GetNumberInput();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO drinking_water(Data, Quantity) VALUES ('{date}', {quantity});";
                tableCmd.ExecuteNonQuery();
                Console.WriteLine("Record inserted successfully.\n");
            }
            GetUserInput();
        }

        private static string GetDateInput()
        {
            MenuManager menuManager = new MenuManager();
            DisplayError displayError = new DisplayError();

            menuManager.DateMenu();

            string date = Console.ReadLine();

            if (date == "0") GetDateInput();

            while (!DateTime.TryParseExact(date, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                displayError.ErrorMessage("invalid_date");
                date = Console.ReadLine();
            }

            return date;
        }

        private static void DeleteRecord()
        {
            Console.Clear();
            ViewAllRecords();

            var recordId = GetNumberInput();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"DELETE FROM drinking_water WHERE Id = {recordId};";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"No record found with the ID {recordId}.\n\n");
                    DeleteRecord();
                }
            }
            Console.WriteLine($"Record {recordId} deleted successfully.\n");
        }

        private static void UpdateRecord()
        {
            Console.Clear();
            ViewAllRecords();

            var recordId = GetNumberInput();

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
                    UpdateRecord();
                }

                string date = GetDateInput();

                int quantity = GetNumberInput();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"UPDATE drinking_water SET Data = '{date}', Quantity = {quantity} WHERE Id = {recordId};";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }


        }

        internal static int GetNumberInput()
        {
            MenuManager menuManager = new MenuManager();
            DisplayError displayError = new DisplayError();

            menuManager.WaterMenu();

            string input = Console.ReadLine();

            if (input == "0") GetUserInput();

            while (!int.TryParse(input, out _) || Convert.ToInt32(input) < 0)
            {
                displayError.ErrorMessage("invalid_input");
                input = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(input);

            return finalInput;
        }
    }


}
