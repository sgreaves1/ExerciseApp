using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace ExerciseApp
{
    internal class ButtonAdapter : BaseAdapter
    {
        private Context context;

        public ButtonAdapter(Context c)
        {
            context = c;
        }

        public override int Count => _thumbIds.Length;

        public override Object GetItem(int position) => null;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var button = new ImageButton(context) {LayoutParameters = new GridView.LayoutParams(200, 200)};
            button.SetScaleType(ImageView.ScaleType.CenterCrop);
            button.SetImageResource(_thumbIds[position]);
            button.Click += ButtonOnClick;
            return button;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var activity2 = new Intent(context, typeof(AddExerciseActivity));
            activity2.PutExtra("ExerciseName", "Push Ups");
            context.StartActivity(activity2);
        }

        private readonly int[] _thumbIds =
        {
            Resource.Drawable.benchpress,
            Resource.Drawable.run,
            Resource.Drawable.situp
        };
    }
}