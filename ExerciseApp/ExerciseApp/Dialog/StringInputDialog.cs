using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace ExerciseApp.Dialog
{
    public class StringInputDialog : DialogFragment
    {
        private EditText _routineNameText;
        private static string _routineName;

        public static StringInputDialog NewInstance(Bundle bundle, string routineName)
        {
            _routineName = routineName;
            var fragment = new StringInputDialog();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.StringInputDialog, container, false);

            GetUiElements(view);

            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            Dialog.SetCanceledOnTouchOutside(false);

            return view;
        }

        private void GetUiElements(View view)
        {
            _routineNameText = view.FindViewById<EditText>(Resource.Id.routineNameText);
            _routineNameText.Text = _routineName;
            _routineNameText.SetSelection(_routineName.Length);
        }
    }
}