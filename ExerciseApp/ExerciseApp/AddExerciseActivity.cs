using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace ExerciseApp
{
    [Activity(Label = "AddExerciseActivity")]
    public class AddExerciseActivity : Activity
    {
        private TextView _exerciseNameLabel;
        private EditText _amount;
        private Button _okButton;
        private Button _cancelButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddExercise);

            // Get the UI controls from the loaded layout
            _exerciseNameLabel = FindViewById<TextView>(Resource.Id.ExerciseName);
            _amount = FindViewById<EditText>(Resource.Id.exerciseAmount);
            _okButton = FindViewById<Button>(Resource.Id.okButton);
            _cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            _okButton.Click += OkButtonOnClick;
            _cancelButton.Click += CancelButtonOnClick;

            // Create your application here
            _exerciseNameLabel.Text = Intent.GetStringExtra("ExerciseName") ?? "Unknown Exercise";

        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {

        }
        
        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }
    }
}