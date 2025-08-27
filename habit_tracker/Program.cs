using System;
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
            SQLGenerator.GenerateSQL(connectionString);
            MenuManager.MainMenu();
            
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
                        SQLRead.ViewAllRecords(connectionString);
                        break;
                    case "2":
                        SQLCreate.CreateRecord(connectionString);
                        break;
                    case "3":
                        SQLDelete.DeleteRecord(connectionString);
                        break;
                    case "4":
                        SQLUpdate.UpdateRecord(connectionString);
                        break;
                    default:
                        DisplayError.ErrorMessage("invalid input");
                        break;
                }
            }
        }
    }
}
