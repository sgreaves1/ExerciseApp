using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExerciseApp.Model;

namespace ExerciseApp.Adapters
{
    class RoutineAdapter : BaseAdapter
    {
        private Context _context;
        private WorkoutRoutine _routine;

        public RoutineAdapter(Context c, WorkoutRoutine routine)
        {
            _context = c;
            _routine = routine;
        }

        public override int Count => _routine.Exercises.Count;

        public override Java.Lang.Object GetItem(int position) => null;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView label = new TextView(_context);
            label.Text = _routine.Exercises[position].Name;
            return label;
        }
    }
}