using System;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace sql_management
{
    public class SQLDelete
    {
        public static void DeleteRecord(string connectionString)
        {
            Console.Clear();
            SQLRead.ViewAllRecords(connectionString);

            var recordId = GetNumberInput();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"DELETE FROM drinking_water WHERE Id = {recordId};";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"No record found with the ID {recordId}.\n\n");
                    DeleteRecord(connectionString);
                }
            }
        }
    }
}