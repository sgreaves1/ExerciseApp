using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using ExerciseApp.Model;

namespace ExerciseApp.Adapters
{
    class RoutineAdapter : BaseAdapter
    {
        private Activity _context;
        private WorkoutRoutine _routine;

        public RoutineAdapter(Activity c, WorkoutRoutine routine)
        {
            _context = c;
            _routine = routine;
        }

        public override int Count => _routine.Exercises.Count;

        public override Java.Lang.Object GetItem(int position) => null;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.ExerciseDetails, null);

            view.FindViewById<TextView>(Resource.Id.exerciseNameLabel).Text = _routine.Exercises[position].Name;
            view.FindViewById<TextView>(Resource.Id.amountLabel).Text = _routine.Exercises[position].Amount.ToString();
            view.FindViewById<TextView>(Resource.Id.WeightLabel).Text = _routine.Exercises[position].Weight.ToString();

            return view;
        }
    }
}