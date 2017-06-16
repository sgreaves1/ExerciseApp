using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Java.IO;

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
        }
    }
}

