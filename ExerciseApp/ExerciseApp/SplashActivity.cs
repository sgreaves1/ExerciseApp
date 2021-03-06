﻿using System.Threading.Tasks;
using Android.App;
using Android.Content;
using ExerciseApp.Data;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", Icon = "@drawable/icon", Theme = "@style/SplashScreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private readonly IDatabase _db = new Database("exercise.db3");
        
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(SimulatedStartup);
            startupWork.Start();
        }

        async void SimulatedStartup()
        {
            // Create db if it doesn't exist
            await Task.Run(() =>
            {
                _db.CreateDatabase();
            });
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}