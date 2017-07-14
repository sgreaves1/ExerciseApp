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
            button.Click += (sender, e) => ButtonOnClick(this, new AddExerciseEventArgs() { Name = _viewItems[position].Name, Gif = _viewItems[position].Gif});
            return button;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var activity2 = new Intent(context, typeof(AddExerciseActivity));
            activity2.PutExtra("ExerciseName", ((AddExerciseEventArgs)eventArgs).Name);
            activity2.PutExtra("ExerciseGif", ((AddExerciseEventArgs)eventArgs).Gif);
            activity2.PutExtra("RoutineId", _routineId);
            context.StartActivity(activity2);
        }

        private readonly AddExerciseViewItem[] _viewItems =
        {
            new AddExerciseViewItem() { Name = "Push Ups", Image =  Resource.Drawable.pushup, Gif =  "https://media.giphy.com/media/tf47T8m4I8Zva/giphy.gif"},
            new AddExerciseViewItem() { Name = "Bicep curls", Image =  Resource.Drawable.BicepCurls, Gif = "https://media.giphy.com/media/ND1r9zoBPgfC/giphy.gif"},
            new AddExerciseViewItem() { Name = "Bench press", Image =  Resource.Drawable.benchpress, Gif = "https://media.giphy.com/media/fuu6c9R2HPB7y/giphy.gif"},
            new AddExerciseViewItem() { Name = "Sit Ups", Image =  Resource.Drawable.situp, Gif = "https://media.giphy.com/media/HnfH76fdZOhP2/giphy.gif" },
            new AddExerciseViewItem() { Name = "Run", Image =  Resource.Drawable.run, Gif = "https://media.giphy.com/media/l2Sqc3POpzkj5r8SQ/giphy.gif" }
        };
    }
}