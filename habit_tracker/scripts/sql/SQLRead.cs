using System;
using models;


namespace sql_management
{
    public class SQLRead
    {
        private readonly string _connectionString;

        public SQLRead(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<DrinkingWater> ViewAllRecords(string tableName)
        {
            return SQLDatabaseHelper.ExecuteQuery(
                    _connectionString,
                    $"SELECT * FROM [{tableName}];",
                    reader => new DrinkingWater
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1)),
                        Quantity = reader.GetInt32(2)
                    }
                );
        }
        
        public List<Habit> ViewAllHabits()
        {
            return SQLDatabaseHelper.ExecuteQuery(
                _connectionString,
                "SELECT * FROM habits;",
                reader => new Habit
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Type = reader.GetString(2),
                }
            );
        }
    }
}