using System;
using Android.App;
using Android.Widget;
using Android.OS;
using ExerciseApp.Data;
using ExerciseApp.Model;
using ExerciseApp.Dialog;
using ExerciseApp.EventArguments;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _dateLabel;
        private TextView _routineLabel;
        private Button _editRoutineNameButton;
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

            // Get the UI controls from the loaded layout
            _dateLabel = FindViewById<TextView>(Resource.Id.dateLabel);
            _routineLabel = FindViewById<TextView>(Resource.Id.routine);
            _editRoutineNameButton = FindViewById<Button>(Resource.Id.editRoutineNameButton);
            _editRoutineNameButton.Click += EditRoutineNameButtonClick;
            _exerciseLabel = FindViewById<TextView>(Resource.Id.exerciseLabel);
            var gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new ButtonAdapter(this);

            PopulateTodaysRoutine();
        }
                
        private void EditRoutineNameButtonClick(object sender, EventArgs e)
        {
            FragmentTransaction fragmemtTransaction = FragmentManager.BeginTransaction();
            // Remove fragment from backstack if any exists
            Fragment fragmentPrev = FragmentManager.FindFragmentByTag("dialog");
            if (fragmentPrev != null)
                fragmemtTransaction.Remove(fragmentPrev);

            fragmemtTransaction.AddToBackStack(null);
            // Create and show the dialog
            StringInputDialog dialogFragment = StringInputDialog.NewInstance(null, _routine.Name);
            dialogFragment.DialogClosed += EditRoutineNameDialogClosed;
            dialogFragment.Show(fragmemtTransaction, "dialog");
        }

        private void PopulateTodaysRoutine()
        {
            _routine = _db.GetTodaysRoutine();
            if (_routine != null)
            {
                _dateLabel.Text = _routine.Date.ToString(@"dd/MM/yy");
                _exerciseLabel.Text = "None";
                _routineLabel.Text = _routine.Name;
            }
            else
            {
                _routine = new WorkoutRoutine();
                _db.InsertData(_routine);
                PopulateTodaysRoutine();
            }
        }

        private void EditRoutineNameDialogClosed(object sender, DialogEventArgs e)
        {
            _routine.Name = e.returnString;
            _db.UpdateData(_routine);
            _routineLabel.Text = _routine.Name;
        }

    }
}

