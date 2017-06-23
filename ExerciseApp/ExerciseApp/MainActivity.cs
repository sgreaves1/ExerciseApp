using System;
using Android.App;
using Android.Widget;
using Android.OS;
using ExerciseApp.Data;
using ExerciseApp.Model;
using System.Collections.Generic;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _dateLabel;
        private TextView _routineLabel;
        private TextView _exerciseLabel;
        private EditText _pushUpsToAdd;
        private TextView _totalLabel;
        private Exercise _todaysData;
        private readonly Database _db = new Database("exercise.db3");

        private WorkoutRoutine _routine; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Create db if it doesn't exist
            _db.CreateDatabase();

            // Get the UI controls from the loaded layout
            _dateLabel = FindViewById<TextView>(Resource.Id.dateLabel);
            _routineLabel = FindViewById<TextView>(Resource.Id.routine);
            _exerciseLabel = FindViewById<TextView>(Resource.Id.exerciseLabel);
            //_pushUpsToAdd = FindViewById<EditText>(Resource.Id.pushUpsToAdd);
            //_totalLabel = FindViewById<TextView>(Resource.Id.totalLabel);
            //Button addButton = FindViewById<Button>(Resource.Id.addButton);
            //Button clearButton = FindViewById<Button>(Resource.Id.clearButton);
            var gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new ImageAdapter(this);
            
            //addButton.Click += AddButtonOnClick;
            //clearButton.Click += ClearButtonOnClick;

            PopulateTodaysRoutine();
        }
        
        private void AddButtonOnClick(object sender, EventArgs eventArgs)
        {
            _todaysData.Amount += int.Parse(_pushUpsToAdd.Text);
            _totalLabel.Text = _todaysData.Amount.ToString();

            SaveAmount();
        }

        private void ClearButtonOnClick(object sender, EventArgs eventArgs)
        {
            _todaysData.Amount = 0;
            _totalLabel.Text = _todaysData.Amount.ToString();

            SaveAmount();
        }
        
        private void SaveAmount()
        {
            _db.UpdateData(_todaysData);
        }
                
        private void PopulateTodaysRoutine()
        {
            _routine = _db.GetTodaysRoutine();
            if (_routine != null)
            {
                _dateLabel.Text = _routine.Date.ToString(@"dd/MM/yy");
                _exerciseLabel.Text = _routine.Name;
                _routineLabel.Text = _routine.Name;
            }
            else
            {
                _routine = new WorkoutRoutine();
                _db.InsertData(_routine);
                PopulateTodaysRoutine();
            }
        }

    }
}

