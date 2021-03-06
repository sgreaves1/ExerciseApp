﻿using Android.App;
using Android.Widget;
using Android.OS;
using ExerciseApp.Data;
using ExerciseApp.Model;
using System.Linq;
using System;
using Android.Content;


namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp")]
    public class MainActivity : Activity
    {
        private TextView _dateLabel;
        private TextView _routineLabel;
        private TextView _exerciseLabel;
        private Button _viewButton;
        private EditText _pushUpsToAdd;
        private TextView _totalLabel;
        private Exercise _todaysData;
        private readonly IDatabase _db = new Database("exercise.db3");

        private WorkoutRoutine _routine;
        private Exercise _currentExercise;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Create db if it doesn't exist
            _db.CreateDatabase();

            // Get today's routine from db
            GetTodaysRotineFromDb();

            // Get the UI controls from the loaded layout
            GetUiElements();

            // Populate the view from the routine model 
            PopulateTodaysRoutine();
        }

        private void GetTodaysRotineFromDb()
        {
            _routine = _db.GetTodaysRoutine();

            if (_routine == null)
            {
                _routine = new WorkoutRoutine();

                _db.InsertData(_routine);

                _routine = _db.GetTodaysRoutine();
            }

            if (_routine.Id > 0)
                _routine.Exercises = _db.GetExercisesByRoutineId(_routine.Id);

            _currentExercise = _routine.Exercises.FirstOrDefault(x => x.Done != true);            
        }

        private void GetUiElements()
        {
            _dateLabel = FindViewById<TextView>(Resource.Id.dateLabel);
            _routineLabel = FindViewById<TextView>(Resource.Id.routine);
            _exerciseLabel = FindViewById<TextView>(Resource.Id.exerciseLabel);
            _viewButton = FindViewById<Button>(Resource.Id.viewRoutineButton);
            _viewButton.Click += ViewRoutineButtonClick;
            var gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new ButtonAdapter(this, _routine.Id);
        }

        private void ViewRoutineButtonClick(object sender, EventArgs e)
        {
            var viewRoutineActivity = new Intent(this, typeof(ViewRoutineActivity));
            viewRoutineActivity.PutExtra("RoutineId", _routine.Id);
            StartActivity(viewRoutineActivity);
        }


        private void PopulateTodaysRoutine()
        {
            if (_routine != null)
            {
                _dateLabel.Text = _routine.Date.ToString(@"dd/MM/yy");
                _exerciseLabel.Text = _currentExercise != null ? _currentExercise.Name : "None";
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

