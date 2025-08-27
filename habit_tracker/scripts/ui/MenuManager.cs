using System;

namespace menu_manager
{
    public class MenuManager
    {
        public static void MainMenu()
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("0. Close Application.");
            Console.WriteLine("1. View All Records.");
            Console.WriteLine("2. Insert Record.");
            Console.WriteLine("3. Delete Record.");
            Console.WriteLine("4. Update Record.");
            Console.Write("-----------------------------------\n");
        }

        public static void DateMenu()
        {
            Console.WriteLine("Enter the date (mm-dd-yy):  Press 0 to return to main menu\n");
        }

        public static void WaterMenu()
        {
            Console.WriteLine("\n\nPlease insert number of glasses or other measure of your choice " + "(no decimals allowed)\n\n");
        }

        public static void UpdateMenu()
        {
            Console.WriteLine("\n\nPlease select the index number of the record you would like to update:  Press 0 to return to main menu\n\n");
        }

        public static void DeleteMenu()
        {
            Console.WriteLine("\n\nPlease select the index number of the record you would like to delete:  Press 0 to return to main menu\n\n");
        }
    }
}