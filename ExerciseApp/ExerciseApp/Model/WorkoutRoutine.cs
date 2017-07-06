using System.Collections.Generic;
using SQLite;
using System;

namespace ExerciseApp.Model
{
    public class WorkoutRoutine
    {
        public WorkoutRoutine()
        {
            Id = 0;
            Name = "None";
            Date = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public List<Exercise> Exercises = new List<Exercise>();
    }
}