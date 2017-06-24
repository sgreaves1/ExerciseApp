using Android.App;
using Android.Widget;
using Android.OS;
using ExerciseApp.Data;
using ExerciseApp.Model;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _dateLabel;
        private TextView _routineLabel;
        private TextView _exerciseLabel;
        private readonly Database _db = new Database("exercise.db3");

        private WorkoutRoutine _routine;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Create db if it doesn't exist
            _db.CreateDatabase();

            // Get todays routine from db
            GetTodaysRotineFromDb();

            // Get the UI controls from the loaded layout
            GetUiElements();

            // Populate the view from the routine model 
            PopulateTodaysRoutine();
        }

        private void GetTodaysRotineFromDb()
        {
            _routine = _db.GetTodaysRoutine();

            if (_routine.ID > 0)
                _routine.Exercises = _db.GetExercisesByRoutineId(_routine.ID);
        }

        private void GetUiElements()
        {
            _dateLabel = FindViewById<TextView>(Resource.Id.dateLabel);
            _routineLabel = FindViewById<TextView>(Resource.Id.routine);
            _exerciseLabel = FindViewById<TextView>(Resource.Id.exerciseLabel);
            var gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new ButtonAdapter(this, _routine.ID);
        }


        private void PopulateTodaysRoutine()
        {
            if (_routine != null)
            {
                _dateLabel.Text = _routine.Date.ToString(@"dd/MM/yy");
                _exerciseLabel.Text = _routine.Name;
                _routineLabel.Text = _routine.Name;
            }
            else
            {
                _routine = new WorkoutRoutine();
                _db.InsertData(_routine);
                PopulateTodaysRoutine();
            }
        }

    }
}

