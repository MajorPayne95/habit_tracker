using System;
using habit_tracker;
using menu_manager;
using error_messages;

namespace sql_management
{
    public class SQLCreate
    {
        public static void CreateRecord(string connectionString, string tableName)
        {
            Console.Clear();
            MenuManager.DateMenu();
            string date = InputManager.GetDateInput();
            MenuManager.WaterMenu();
            int quantity = Convert.ToInt32(InputManager.GetUserInput());

            string type = SQLDatabaseHelper.GetHabitType(connectionString, tableName) ?? "units";

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"INSERT INTO [{tableName}](date, quantity, type) VALUES (@Date, @Quantity, @type);",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@type", type);
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

            string tableName = SQLDatabaseHelper.GenerateTableName(name);

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"INSERT INTO habits(name, type, tableName) VALUES (@name, @type, @tableName);",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@tableName", tableName);
                }
            );
            
            SQLGenerator.GenerateSQLTable(connectionString, tableName);
        }
    }
}