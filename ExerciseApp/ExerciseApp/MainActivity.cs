using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _pushUpsDone = 0;
        private EditText _pushUpsToAdd;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get the UI controls from the loaded layout
            _pushUpsToAdd = FindViewById<EditText>(Resource.Id.pushUpsToAdd);
            Button addButton = FindViewById<Button>(Resource.Id.addButton);
            addButton.Click += AddButtonOnClick;

            EditText totalLabel = FindViewById<EditText>(Resource.Id.totalLabel);
            totalLabel.Text = _pushUpsDone.ToString();
        }

        private void AddButtonOnClick(object sender, EventArgs eventArgs)
        {
            _pushUpsDone += int.Parse(_pushUpsToAdd.Text);
        }
    }
}

