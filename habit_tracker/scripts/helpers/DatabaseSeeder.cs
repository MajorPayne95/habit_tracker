using System;
using Microsoft.Data.Sqlite;

namespace sql_management
{
    public static class DatabaseSeeder
    {
        public static void Seed(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                var startDate = new DateTime(1990, 01, 01);

                var habits = new[]
                {
                    new { Name = "Drink Water", Type = "fl oz", TableName = "drinking_water" },
                    new { Name = "Exercise", Type = "calories", TableName = "exercise" },
                    new { Name = "Read Book", Type = "count", TableName = "reading" }
                };

                foreach (var habit in habits)
                {
                    command.CommandText = @"
                        INSERT OR IGNORE INTO habits (name, type, tablename)
                        VALUES ($name, $type, $tableName);
                    ";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("$name", habit.Name);
                    command.Parameters.AddWithValue("$type", habit.Type);
                    command.Parameters.AddWithValue("$tableName", habit.TableName);
                    command.ExecuteNonQuery();

                    // Create corresponding table if it doesn't exist
                    command.CommandText = $@"
                        CREATE TABLE IF NOT EXISTS [{habit.TableName}] (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            date TEXT NOT NULL,
                            quantity INTEGER NOT NULL,
                            type TEXT NOT NULL
                        );
                    ";
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();

                    // Seed 10 records for each habit
                    for (int i = 0; i < 10; i++)
                    {
                        int random = new Random().Next(1, 18250);
                        var date = startDate.AddDays(random).ToString("MM-dd-yy");
                        var quantity = (i + 1) * random; // Example quantity
                        command.CommandText = $@"
                            INSERT INTO [{habit.TableName}] (date, quantity, type)
                            VALUES ($date, $quantity, $type);
                        ";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("$date", date);
                        command.Parameters.AddWithValue("$quantity", quantity);
                        command.Parameters.AddWithValue("$type", habit.Type);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}