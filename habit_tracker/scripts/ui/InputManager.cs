using System.Globalization;
using error_messages;

namespace habit_tracker
{
    public class InputManager
    {
        public static string GetUserInput()
        {
            string? input = Console.ReadLine();

            while (!int.TryParse(input, out _) || Convert.ToInt32(input) < 0)
            {
                DisplayError.ErrorMessage("invalid_input");
                input = Console.ReadLine();
            }

            return input;
        }

        public static string GetDateInput()
        {
            string? date = Console.ReadLine();

            if (date == "0") GetDateInput();

            while (!DateTime.TryParseExact(date, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                DisplayError.ErrorMessage("invalid_date");
                date = Console.ReadLine();
            }

            return date;
        }
    }
}