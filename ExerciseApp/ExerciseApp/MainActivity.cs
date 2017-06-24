using System;
using Android.App;
using Android.Widget;
using Android.OS;
using ExerciseApp.Data;
using ExerciseApp.Model;
using System.Collections.Generic;
using System.Linq;
using Android.Views;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _totalLabel;
        private Exercise _todaysData;
        private readonly Database _db = new Database("exercise.db3");

        private List<WorkoutRoutine> _routine;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Create db if it doesn't exist
            _db.CreateDatabase();

            PopulateTodaysRoutine();

            // Get the UI controls from the loaded layout
            var gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new ButtonAdapter(this);

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.Adapter = new ListViewAdapter(this, _routine);

        }


        private void PopulateTodaysRoutine()
        {
            _routine = _db.GetTodaysRoutine();

            if (!_routine.Any())
            {
                var toAdd = new WorkoutRoutine();
                _db.InsertData(toAdd);
                PopulateTodaysRoutine();
            }
        }

    }


}

