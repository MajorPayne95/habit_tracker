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
            SQLGenerator.GenerateSQL(connectionString);
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
                        sqlReader.ViewAllRecords();
                        MenuManager.ExitPage();
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
