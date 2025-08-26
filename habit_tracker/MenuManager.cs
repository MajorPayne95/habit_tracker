using System;

namespace menu_manager
{
    public class MenuManager
    {
        public void MainMenu()
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

        public void DateMenu()
        {
            Console.WriteLine("Enter the date (mm-dd-yy):  Press 0 to return to main menu\n");
        }

        public void WaterMenu()
        {
            Console.WriteLine("\n\nPlease insert number of glasses or other measure of your choice " + "(no decimals allowed)\n\n");
        }
    }
}