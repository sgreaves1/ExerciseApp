using Android.App;
using Android.OS;
using Android.Widget;
using System;
using ExerciseApp.Model;
using ExerciseApp.Data;

namespace ExerciseApp
{
    [Activity(Label = "AddExerciseActivity")]
    public class AddExerciseActivity : Activity
    {
        private readonly Database _db = new Database("exercise.db3");

        private TextView _exerciseNameLabel;
        private EditText _amount;
        private Button _okButton;
        private Button _cancelButton;

        private int _routineId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddExercise);

            // Get the UI controls from the loaded layout
            GetUiElements();

            // Create your application here
            _exerciseNameLabel.Text = Intent.GetStringExtra("ExerciseName") ?? "Unknown Exercise";
            _routineId = Intent.GetIntExtra("RoutineId", 0);
        }

        private void GetUiElements()
        {
            _exerciseNameLabel = FindViewById<TextView>(Resource.Id.ExerciseName);
            _amount = FindViewById<EditText>(Resource.Id.exerciseAmount);
            _okButton = FindViewById<Button>(Resource.Id.okButton);
            _cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            _okButton.Click += OkButtonOnClick;
            _cancelButton.Click += CancelButtonOnClick;
        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (int.Parse(_amount.Text) != 0)
            {
                Exercise exercise = new Exercise()
                {
                    RoutineId = _routineId,
                    Name = _exerciseNameLabel.Text,
                    Date = DateTime.Now,
                    Amount = int.Parse(_amount.Text)
                };

                _db.InsertData(exercise);

                Finish();
            }


        }
        
        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }
    }
}