using Android.App;
using Android.OS;
using Android.Widget;

namespace ExerciseApp
{
    [Activity(Label = "ViewRoutineActivity")]
    public class ViewRoutineActivity : Activity
    {
        private TextView _routineNameLabel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "ViewRoutine" layout resource
            SetContentView(Resource.Layout.ViewRoutine);

            GetUiElements();
        }

        private void GetUiElements()
        {
            _routineNameLabel = FindViewById<TextView>(Resource.Id.routine);
        }

    }
}