using System;
using menu_manager;
using error_messages;
using sql_management;

namespace habit_tracker
{
    class Program
    {
        static readonly string connectionString = @"Data Source=habit-Tracker.db";

        static void Main()
        {
            SQLGenerator.GenerateHabitTable(connectionString);
            //SQLGenerator.GenerateSQL(connectionString);

            MenuManager.MainMenu();
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
                        Environment.Exit(0);
                        break;
                    case "1":
                        sqlReader.ViewAllHabits();
                        MenuManager.EnterHabit();
                        var selection = InputManager.GetHabitSelection();
                        DisplayHabitRecords(connectionString, selection);
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
                        DisplayError.ErrorMessage("invalid input");
                        break;
                }
            }
        }

        public static void DisplayHabitRecords(string connectionString, string tableName)
        {
            MenuManager.HabitMenu();
            SQLRead sqlReader = new SQLRead(connectionString);

            string? choice = InputManager.GetUserInput();

            switch (choice)
                {
                    case "0":
                        MenuManager.MainMenu();
                        break;
                    case "1":
                        sqlReader.ViewAllRecords(tableName);
                        break;
                    case "2":
                        SQLCreate.CreateRecord(connectionString);
                        break;
                    case "3":
                        SQLUpdate.UpdateRecord(connectionString, tableName);
                        break;
                    case "4":
                        SQLDelete.DeleteRecord(connectionString, tableName);
                        break;
                    default:
                        DisplayError.ErrorMessage("invalid input");
                        break;
                }
        }
    }
}
