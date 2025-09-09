using System;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLCreate
    {
        public static void CreateRecord(string connectionString)
        {
            Console.Clear();
            MenuManager.DateMenu();
            string date = InputManager.GetDateInput();
            MenuManager.WaterMenu();
            int quantity = Convert.ToInt32(InputManager.GetUserInput());

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"INSERT INTO drinking_water(Data, Quantity) VALUES (@Date, @Quantity);",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                }
            );
        }

        public static void CreateHabit(string connectionString)
        {
            Console.Clear();
            MenuManager.HabitNameMenu();
            string name = InputManager.GetHabitInput();
            MenuManager.HabitTypeMenu();
            string type = InputManager.GetHabitInput();
            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"INSERT INTO habits(Name, Type) VALUES (@Name, @Type);",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Type", type);
                    //cmd.Parameters.AddWithValue("@Quantity", quantity);
                }
            );
            SQLGenerator.GenerateSQLTable(connectionString, name);
        }
    }  
}