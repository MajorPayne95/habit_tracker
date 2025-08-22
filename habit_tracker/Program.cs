using System;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace habit_tracker
{
    class Program
    {
        static string connectionString = @"Data Source=habit-Tracker.db";

        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Data TEXT,
                        Quantity INTEGER
                        );";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
            GetUserInput();
        }

        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (!closeApp)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("0. Close Application.");
                Console.WriteLine("1. View All Records.");
                Console.WriteLine("2. Insert Record.");
                Console.WriteLine("3. Delete Record.");
                Console.WriteLine("4. Update Record.");
                Console.Write("-----------------------------------\n");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.WriteLine("Closing application...\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        ViewAllRecords();
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
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void ViewAllRecords()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    "SELECT * FROM drinking_water;";

                List<DrinkingWater> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add
                        (
                            new DrinkingWater
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-mm-yy", new CultureInfo("en-US")),
                                Quantity = reader.GetInt32(2)
                            }
                        );
                    }
                }
                else
                {
                    Console.WriteLine("No records found.");
                }

                connection.Close();

                Console.WriteLine("------------------------------------");
                foreach (var dw in tableData)
                {
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-mmm-yyyy")} - Quantity: {dw.Quantity}");
                }
            }
        }

        private static void InsertNewRecord()
        {
            string date = GetDateInput();

            int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice " +
                "(no decimals allowed)\n\n");

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
            Console.WriteLine("Enter the date (mm-dd-yy):  Press 0 to return to main menu\n");
            string date = Console.ReadLine();

            if (date == "0") GetDateInput();

            while (!DateTime.TryParseExact(date, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid date format. Please enter the date in mm-dd-yy format.");
                date = Console.ReadLine();
            }

            return date;
        }

        private static void DeleteRecord()
        {
            Console.Clear();
            ViewAllRecords();

            var recordId = GetNumberInput("\n\nPlease enter the ID of the record you want to delete, or enter 0 to return to the Main Menun\n\n");

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

            var recordId = GetNumberInput("\n\nPlease enter the ID of the record you want to update, or enter 0 to return to the Main Menun\n\n");

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

                int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice (no decimals allowed)\n\n");

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"UPDATE drinking_water SET Data = '{date}', Quantity = {quantity} WHERE Id = {recordId};";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }


        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (input == "0") GetUserInput();

            while (!int.TryParse(input, out _) || Convert.ToInt32(input) < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                input = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(input);

            return finalInput;
        }
    }

    public class DrinkingWater
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}
