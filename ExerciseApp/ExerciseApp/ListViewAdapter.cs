using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using ExerciseApp.Model;
using Object = Java.Lang.Object;

namespace ExerciseApp
{
    class ListViewAdapter : BaseAdapter<WorkoutRoutine>
    {
        private List<WorkoutRoutine> _data;
        private Activity _activity;

        public ListViewAdapter(Activity activity, List<WorkoutRoutine> data)
        {
            _activity = activity;
            _data = data;
        }

        public override int Count => _data.Count;

        public override WorkoutRoutine this[int position] => _data[position];

        public override Object GetItem(int position) => null;

        public override long GetItemId(int position) => position;


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _data[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = _activity.LayoutInflater.Inflate(Resource.Layout.RoutineLayout, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name;
            view.FindViewById<Button>(Resource.Id.Button).Text = "AddSomething";
            return view;
        }
    }
}