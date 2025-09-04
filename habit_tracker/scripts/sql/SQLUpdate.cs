using System;
using habit_tracker;
using menu_manager;

namespace sql_management
{
    public class SQLUpdate
    {
        public static void UpdateRecord(string connectionString)
        {
            Console.Clear();
            SQLRead sqlRead = new SQLRead(connectionString);
            sqlRead.ViewAllRecords();
            
            MenuManager.UpdateMenu();
            var recordId = Convert.ToInt32(InputManager.GetUserInput());

            MenuManager.DateMenu();
            string date = InputManager.GetDateInput();

            MenuManager.WaterMenu();
            int quantity = Convert.ToInt32(InputManager.GetUserInput());

            SQLDatabaseHelper.ExecuteNonQuery(
                connectionString,
                $"UPDATE drinking_water SET Data = @Date, Quantity = @Quantity WHERE Id =@Id;",
                cmd => {
                    cmd.Parameters.AddWithValue("@Id", recordId);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                }
            );
        }
    }  
}