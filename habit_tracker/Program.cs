using System;
using menu_manager;
using sql_management;
using error_messages;

namespace habit_tracker
{
    class Program
    {
        static readonly string connectionString = @"Data Source=habit-Tracker.db";

        static void Main()
        {
            SQLGenerator.GenerateHabitTable(connectionString);
            DatabaseSeeder.Seed(connectionString);

            SQLRead sqlReader = new SQLRead(connectionString);
            bool closeApp = false;

            while (!closeApp)
            {
                MenuManager.MainMenu();
                string? choice = InputManager.GetUserInput();

                switch (choice)
                {
                    case "0":
                        Console.WriteLine("Closing application...\n");
                        closeApp = true;
                        break;

                    case "1":
                        string? tableName = HabitSelector.GetTableNameFromUserSelection(sqlReader);
                        if (tableName != null)
                            DisplayHabitRecords(connectionString, tableName);
                        break;

                    case "2":
                        SQLCreate.CreateHabit(connectionString);
                        break;

                    case "3":
                        SQLUpdate.UpdateHabit(connectionString);
                        break;

                    case "4":
                        SQLDelete.DeleteHabit(connectionString);
                        break;

                    default:
                        DisplayError.ErrorMessage("invalid_choice");
                        break;
                }
            }
        }

        public static void DisplayHabitRecords(string connectionString, string tableName)
        {
            SQLRead sqlReader = new SQLRead(connectionString);

            while (true)
            {
                MenuManager.HabitMenu();
                string? choice = InputManager.GetUserInput();

                switch (choice)
                {
                    case "0":
                        return; // Return to main menu
                    case "1":
                        sqlReader.ViewAllRecords(tableName);
                        Console.ReadLine(); // Pause to let user read records
                        break;
                    case "2":
                        SQLCreate.CreateRecord(connectionString, tableName);
                        break;
                    case "3":
                        SQLUpdate.UpdateRecord(connectionString, tableName);
                        break;
                    case "4":
                        SQLDelete.DeleteRecord(connectionString, tableName);
                        break;
                    default:
                        DisplayError.ErrorMessage("invalid_choice");
                        break;
                }
            }
        }
    }
}
