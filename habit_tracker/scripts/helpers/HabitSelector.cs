using sql_management;
using menu_manager;
using error_messages;

namespace habit_tracker
{
    public static class HabitSelector
    {
        public static string? GetTableNameFromUserSelection(SQLRead sqlReader)
        {
            var habits = sqlReader.ViewAllHabits();
            if (habits.Count == 0)
            {
                Console.WriteLine("No habits found.");
                return null;
            }

            Console.WriteLine("\nAvailable Habits:");
            for (int i = 0; i < habits.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {habits[i].Name}");
            }

            Console.WriteLine("\nSelect a habit by number (0 to return to main menu): ");
            int index = -1;

            while (true)
            {
                string input = InputManager.GetUserInput();

                if (input == "0")
                    return null; // Return to main menu

                if (int.TryParse(input, out index) && index >= 1 && index <= habits.Count)
                    break;

                DisplayError.ErrorMessage("invalid_choice");
            }

            // Return actual table name
            return habits[index - 1].TableName;
        }
    }
}
