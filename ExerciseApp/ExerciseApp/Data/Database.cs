using System;
using System.IO;
using ExerciseApp.Model;
using SQLite;
using Environment = System.Environment;

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

        public string CreateDatabase()
        {
            try
            {
                var connection = new SQLiteConnection(DatabaseLocation);
                connection.CreateTable<Exercise>();
                return "Database created";

            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public string InsertUpdateData(Exercise data)
        {
            try
            {
                var db = new SQLiteConnection(DatabaseLocation);
                if (db.Insert(data) != 0)
                    db.Update(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}