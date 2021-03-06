﻿using System;
using ExerciseApp.Eunmerators;
using SQLite;

namespace ExerciseApp.Model
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }

        public int RoutineId { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public double Weight { get; set; }

        public WeightUnits WeightUnits { get; set; }

        public DateTime Date { get; set; }

        public bool Done { get; set; }

        public Exercise()
        {
            Id = 0;
            RoutineId = 0;
            Name = "Push-Ups";
            Amount = 0;
            Weight = 0;
            Date = DateTime.Today;
            Done = false;
        }
    }
}