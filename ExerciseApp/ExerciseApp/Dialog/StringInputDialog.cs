using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using Android.Content;
using ExerciseApp.EventArguments;

namespace ExerciseApp.Dialog
{
    public class StringInputDialog : DialogFragment
    {
        private EditText _routineNameText;
        private Button _okButton;
        private Button _cancelButton;
        
        private static string _routineName;
        private bool _okPressed;

        public event EventHandler<DialogEventArgs> DialogClosed;

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

            _okButton = view.FindViewById<Button>(Resource.Id.okButton);
            _okButton.Click += OkClicked;

            _cancelButton = view.FindViewById<Button>(Resource.Id.cancelButton);
            _cancelButton.Click += CancelClicked;
        }

        private void OkClicked(object sender, EventArgs e)
        {
            _okPressed = true;
            Dismiss();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            _okPressed = false;
            Dismiss();
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);

            if (_okPressed)
                DialogClosed?.Invoke(this, new DialogEventArgs() { returnString = _routineNameText.Text });
        }
    }
}