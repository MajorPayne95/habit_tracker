using System;
using Microsoft.Data.Sqlite;

namespace sql_management
{
    public static class SQLDatabaseHelper
    {
        public static List<T> ExecuteQuery<T>(
            string connectionString,
            string sql,
            Func<SqliteDataReader, T> mapFunction,
            Action<SqliteCommand>? parameterize = null
        )
        {
            var results = new List<T>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;

                parameterize?.Invoke(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(mapFunction(reader));
                        Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetString(2)}");
                    }
                    return results;
                }
            }
        }

        public static int ExecuteNonQuery(
            string connectionString,
            string sql,
            Action<SqliteCommand>? parameterize = null
        )
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;

                parameterize?.Invoke(command);

                return command.ExecuteNonQuery();
                // Returns number of rows affected
            }
        }
    }

}