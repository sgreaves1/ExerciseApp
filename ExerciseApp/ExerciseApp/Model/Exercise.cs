using System;
using SQLite;

namespace ExerciseApp.Model
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public Exercise()
        {
            Amount = 0;
            Date = DateTime.Today;
        }
    }
}