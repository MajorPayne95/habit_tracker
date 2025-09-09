using System;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLDelete
    {
        public static void DeleteRecord(string connectionString, string tableName)
        {
            Console.Clear();
            SQLRead sqlRead = new SQLRead(connectionString);
            sqlRead.ViewAllRecords(tableName);

            MenuManager.DeleteMenu();
            var recordId = Convert.ToInt32(InputManager.GetUserInput());

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"DELETE FROM drinking_water WHERE Id = @Id;",
                cmd => cmd.Parameters.AddWithValue("@Id", recordId)
            );
        }

        public static void DeleteHabit(string connectionString)
        {
            Console.Clear();
            SQLRead sqlRead = new SQLRead(connectionString);
            sqlRead.ViewAllHabits();

            MenuManager.DeleteMenu();
            var habitId = Convert.ToInt32(InputManager.GetUserInput());

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"DELETE FROM habits WHERE Id = @Id;",
                cmd => cmd.Parameters.AddWithValue("@Id", habitId)
            );
        }
    }  
}