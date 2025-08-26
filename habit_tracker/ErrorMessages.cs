using System;

namespace error_messages
{
    public class DisplayError
    {
        public void ErrorMessage(string error)
        {
            switch (error)
            {
                case "invalid_choice":
                    Console.WriteLine("Invalid input, please enter a valid number");
                    break;
                case "invalid_date":
                    Console.WriteLine("Invalid date format. Please enter the date in mm-dd-yy format.");
                    break;
            }
        }
    }
}