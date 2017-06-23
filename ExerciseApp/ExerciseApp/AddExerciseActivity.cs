using Android.App;
using Android.OS;
using Android.Widget;

namespace ExerciseApp
{
    [Activity(Label = "AddExerciseActivity")]
    public class AddExerciseActivity : Activity
    {
        private TextView _exerciseNameLabel;  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddExercise);

            // Get the UI controls from the loaded layout
            _exerciseNameLabel = FindViewById<TextView>(Resource.Id.ExerciseName);

            // Create your application here
            _exerciseNameLabel.Text = Intent.GetStringExtra("ExerciseName") ?? "Unknown Exercise";
        }
    }
}