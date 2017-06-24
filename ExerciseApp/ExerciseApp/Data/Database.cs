using System;
using System.IO;
using ExerciseApp.Model;
using SQLite;
using Environment = System.Environment;
using System.Collections.Generic;

namespace ExerciseApp.Data
{
    public class Database
    {
        public string DatabaseLocation { get; set; }

        public Database(string path)
        {
            DatabaseLocation = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);
        }

        public List<WorkoutRoutine> GetRoutines()
        {
            List<WorkoutRoutine> routines = new List<WorkoutRoutine>();

            try
            {
                var db = new SQLiteConnection(DatabaseLocation);

                var tabel = db.Table<WorkoutRoutine>();

                foreach (var item in tabel)
                {
                    item.Exercises = GetExercisesByRoutineId(item.ID);
                    routines.Add(item);
                }
            }
            catch
            {

            }

            return routines;
        }

        public Exercise GetTodaysData()
        {
            var db = new SQLiteConnection(DatabaseLocation);

            var tabel = db.Table<Exercise>();

            foreach (var item in tabel)
            {
                if (item.Date.Year == DateTime.Today.Year)
                    if (item.Date.Month == DateTime.Today.Month)
                        if (item.Date.Day == DateTime.Today.Day)
                        {
                            Exercise exercise = new Exercise
                            {
                                Amount = item.Amount,
                                Date = item.Date,
                                ID = item.ID,
                                Name = item.Name
                            };

                            return exercise;
                        }
            }

            return null;
        }

        public WorkoutRoutine GetTodaysRoutine()
        {
            try
            {
                var db = new SQLiteConnection(DatabaseLocation);

                var tabel = db.Table<WorkoutRoutine>();

                foreach (var item in tabel)
                {
                    if (item.Date.Year == DateTime.Today.Year)
                        if (item.Date.Month == DateTime.Today.Month)
                            if (item.Date.Day == DateTime.Today.Day)
                            {
                                WorkoutRoutine routine = new WorkoutRoutine
                                {
                                    Date = item.Date,
                                    ID = item.ID,
                                    Name = item.Name
                                };

                                return routine;
                            }
                }
            }
            catch
            {

            }

            return null;
        }

        public List<Exercise> GetExercisesByRoutineId(int routineId)
        {
            List<Exercise> exercises = new List<Exercise>();
            try
            {
                var db = new SQLiteConnection(DatabaseLocation);

                var tabel = db.Table<Exercise>();

                foreach (var item in tabel)
                {
                    if (item.RoutineId == routineId)
                    {
                        exercises.Add(item);
                    }
                }
            }
            catch
            {

            }

            return exercises;
        }

        public string CreateDatabase()
        {
            try
            {
                var connection = new SQLiteConnection(DatabaseLocation);
                connection.CreateTable<Exercise>();
                connection.CreateTable<WorkoutRoutine>();
                return "Database created";

            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public string InsertData(WorkoutRoutine data)
        {
            try
            {
                if (data.ID == 0)
                {
                    var db = new SQLiteConnection(DatabaseLocation);
                    db.Insert(data);
                    return "New Routine inserted";
                }
                return "ID not 0, cant insert routine";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public string InsertData(Exercise data)
        {
            try
            {
                if (data.ID == 0)
                {
                    var db = new SQLiteConnection(DatabaseLocation);
                    db.Insert(data);
                    return "New exercise inserted";
                }
                return "ID not 0, cant insert exercise";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public string UpdateData(Exercise data)
        {
            try
            {
                var db = new SQLiteConnection(DatabaseLocation);
                db.Update(data);
                return "exercise updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public void DisplayAllDbData()
        {
            List<WorkoutRoutine> routines = GetRoutines();

            foreach (var routine in routines)
            {
                Console.WriteLine("Routine: " + routine.ID + " " + routine.Date);
                foreach (var exercise in routine.Exercises)
                    Console.WriteLine("       Exercise: " + exercise.Name + " " + exercise.Amount);

                Console.WriteLine("");
            }
        }
    }
}