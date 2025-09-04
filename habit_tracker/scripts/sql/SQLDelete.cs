using System;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLDelete
    {
        public static void DeleteRecord(string connectionString)
        {
            Console.Clear();
            SQLRead sqlRead = new SQLRead(connectionString);
            sqlRead.ViewAllRecords();

            MenuManager.DeleteMenu();
            var recordId = Convert.ToInt32(InputManager.GetUserInput());

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"DELETE FROM drinking_water WHERE Id = @Id;",
                cmd => cmd.Parameters.AddWithValue("@Id", recordId)
            );
        }
    }  
}