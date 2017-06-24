using System;
using System.Collections.Generic;
using System.IO;
using ExerciseApp.Model;
using SQLite;
using Environment = System.Environment;

namespace ExerciseApp.Data
{
    public class Database
    {
        // I love lima, dave and all me unknown viwers, please make yourself known
        public string DatabaseLocation { get; set; }

        public Database(string path)
        {
            DatabaseLocation = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);
        }

        //Testing Committing!
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

        public List<WorkoutRoutine> GetTodaysRoutine()
        {
            using (var db = new SQLiteConnection(DatabaseLocation))
            {
                var toReturn = new List<WorkoutRoutine>();

                var table = db.Table<WorkoutRoutine>();

                foreach (var item in table)
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

                                toReturn.Add(routine);
                            }
                }

                return toReturn;
            }
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
                    return "Single data file inserted";
                }
                return "ID not 0, cant insert";
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
                return "Single data file updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}