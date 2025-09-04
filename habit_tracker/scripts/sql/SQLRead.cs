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
        
        public List<DrinkingWater> ViewAllRecords()
        {
            return SQLDatabaseHelper.ExecuteQuery(
                _connectionString,
                "SELECT * FROM drinking_water;",
                reader => new DrinkingWater
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1)),
                    Quantity = reader.GetInt32(2)
                }
            );
            
        }
    }
}