using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExerciseApp.Data;

namespace ExerciseApp
{
    [Activity(Label = "SplashActivity", Theme = "@style/SplashScreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private readonly Database _db = new Database("exercise.db3");

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