using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

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
            return button;
        }

        private readonly int[] _thumbIds =
        {
            Resource.Drawable.benchpress,
            Resource.Drawable.run,
            Resource.Drawable.situp
        };
    }
}