using System;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using ExerciseApp.Model;
using Java.IO;
using SQLite;
using Environment = System.Environment;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _pushUpsDone = 0;
        private EditText _pushUpsToAdd;
        private TextView totalLabel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get the UI controls from the loaded layout
            _pushUpsToAdd = FindViewById<EditText>(Resource.Id.pushUpsToAdd);
            totalLabel = FindViewById<TextView>(Resource.Id.totalLabel);
            Button addButton = FindViewById<Button>(Resource.Id.addButton);
            Button clearButton = FindViewById<Button>(Resource.Id.clearButton);
            
            addButton.Click += AddButtonOnClick;
            clearButton.Click += ClearButtonOnClick;

            totalLabel.Text = _pushUpsDone.ToString();

            GetData();
        }
        
        private void AddButtonOnClick(object sender, EventArgs eventArgs)
        {
            _pushUpsDone += int.Parse(_pushUpsToAdd.Text);
            totalLabel.Text = _pushUpsDone.ToString();

            SaveAmount();
        }

        private void ClearButtonOnClick(object sender, EventArgs eventArgs)
        {
            _pushUpsDone = 0;
            totalLabel.Text = _pushUpsDone.ToString();

            SaveAmount();
        }

        private void SaveAmount()
        {
            // Add save code here
            string dbPath = Path.Combine(
                System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "exercise.db3");

            CreateDatabase(dbPath);
            InsertUpdateData(new Exercise() {Amount = _pushUpsDone, Date = DateTime.Now, Name = "PushUps"}, dbPath);

        }

        private void GetData()
        {
            string dbPath = Path.Combine(
                System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "exercise.db3");

            var db = new SQLiteConnection(dbPath);

            var tabel = db.Table<Exercise>();

            foreach (var item in tabel)
            {
                if (item.Date.Year == DateTime.Today.Year)
                    if (item.Date.Month == DateTime.Today.Month)
                        if (item.Date.Day == DateTime.Today.Day)
                        {
                            _pushUpsDone = item.Amount;
                            totalLabel.Text = _pushUpsDone.ToString();
                        }
            }
        }

        private string CreateDatabase(string path)
        {
            try
            {
                var connection = new SQLiteConnection(path);
                connection.CreateTable<Exercise>();
                return "Database created";

            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private string InsertUpdateData(Exercise data, string path)
        {
            try
            {
                var db = new SQLiteConnection(path);
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

