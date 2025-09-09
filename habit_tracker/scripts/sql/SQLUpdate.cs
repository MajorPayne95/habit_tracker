using System;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLUpdate
    {
        public static void UpdateRecord(string connectionString, string tableName)
        {
            Console.Clear();
            SQLRead sqlRead = new SQLRead(connectionString);
            sqlRead.ViewAllRecords(tableName);

            MenuManager.UpdateMenu();
            var recordId = Convert.ToInt32(InputManager.GetUserInput());

            MenuManager.DateMenu();
            string date = InputManager.GetDateInput();

            MenuManager.WaterMenu();
            int quantity = Convert.ToInt32(InputManager.GetUserInput());

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"UPDATE drinking_water SET Data = @Date, Quantity = @Quantity WHERE Id =@Id;",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", recordId);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                }
            );
        }
        
        public static void UpdateHabit(string connectionString)
        {
            Console.Clear();
            SQLRead sqlRead = new SQLRead(connectionString);
            sqlRead.ViewAllHabits();

            MenuManager.UpdateMenu();
            var habitId = Convert.ToInt32(InputManager.GetUserInput());

            MenuManager.HabitNameMenu();
            string habitName = InputManager.GetHabitInput();

            MenuManager.HabitTypeMenu();
            string habitType = InputManager.GetHabitInput();

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"UPDATE habits SET Name = @Name, Type = @Type WHERE Id =@Id;",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", habitId);
                    cmd.Parameters.AddWithValue("@Name", habitName);
                    cmd.Parameters.AddWithValue("@Type", habitType);
                }
            );
        }
    }  
}