using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;
using ExerciseApp.Model;
using ExerciseApp.EventArguments;

namespace ExerciseApp
{
    internal class ButtonAdapter : BaseAdapter
    {
        private Context context;
        private int _routineId;

        public ButtonAdapter(Context c, int RoutineId)
        {
            context = c;
            _routineId = RoutineId;
        }

        public override int Count => _viewItems.Length;

        public override Object GetItem(int position) => null;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var button = new ImageButton(context) {LayoutParameters = new GridView.LayoutParams(200, 200)};
            button.SetScaleType(ImageView.ScaleType.CenterCrop);
            button.SetImageResource(_viewItems[position].Image);
            button.Click += (sender, e) => ButtonOnClick(this, new AddExerciseEventArgs() { Name = _viewItems[position].Name });
            return button;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var activity2 = new Intent(context, typeof(AddExerciseActivity));
            activity2.PutExtra("ExerciseName", ((AddExerciseEventArgs)eventArgs).Name);
            activity2.PutExtra("RoutineId", _routineId);
            context.StartActivity(activity2);
        }

        private readonly AddExerciseViewItem[] _viewItems =
        {
            new AddExerciseViewItem() { Name = "Push Ups", Image =  Resource.Drawable.benchpress },
            new AddExerciseViewItem() { Name = "Sit Ups", Image =  Resource.Drawable.situp },
            new AddExerciseViewItem() { Name = "Run", Image =  Resource.Drawable.run }
        };
    }
}