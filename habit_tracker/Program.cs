using System;
using System.Globalization;
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
                DisplayError displayError = new DisplayError();
                MenuManager.MainMenu();

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
                        SQLCreate.CreateRecord(connectionString);
                        //InsertNewRecord();
                        break;
                    case "3":
                        SQLDelete.DeleteRecord(connectionString);
                        //DeleteRecord();
                        break;
                    case "4":
                        SQLUpdate.UpdateRecord(connectionString);
                        break;
                    default:
                        displayError.ErrorMessage("invalid input");
                        break;
                }
            }
        }

        public static string GetDateInput()
        {
            MenuManager menuManager = new MenuManager();
            DisplayError displayError = new DisplayError();

            MenuManager.DateMenu();

            string date = Console.ReadLine();

            if (date == "0") GetDateInput();

            while (!DateTime.TryParseExact(date, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                displayError.ErrorMessage("invalid_date");
                date = Console.ReadLine();
            }

            return date;
        }

        public static int GetNumberInput()
        {
            MenuManager menuManager = new MenuManager();
            DisplayError displayError = new DisplayError();

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
