using Android.App;
using Android.OS;
using Android.Widget;
using ExerciseApp.Adapters;
using ExerciseApp.Data;
using ExerciseApp.Model;

namespace ExerciseApp
{
    [Activity(Label = "ViewRoutineActivity")]
    public class ViewRoutineActivity : Activity
    {
        private TextView _routineNameLabel;
        private ListView _exerciseList;

        private int _routineId;
        private WorkoutRoutine _routine;

        private readonly Database _db = new Database("exercise.db3");


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "ViewRoutine" layout resource
            SetContentView(Resource.Layout.ViewRoutine);

            _routineId = Intent.GetIntExtra("RoutineId", 0);

            if (_routineId > 0)
            {
                _routine = _db.GetRoutineById(_routineId);
                _routine.Exercises = _db.GetExercisesByRoutineId(_routineId);
            }

            GetUiElements();
        }

        private void GetUiElements()
        {
            _routineNameLabel = FindViewById<TextView>(Resource.Id.routine);
            _routineNameLabel.Text = _routine.Name;

            _exerciseList = FindViewById<ListView>(Resource.Id.exerciseList);
            _exerciseList.Adapter = new RoutineAdapter(this, _routine);

        }

    }
}