using System;

namespace models
{
    public class DrinkingWater
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string? Type { get; set; }
    }
}