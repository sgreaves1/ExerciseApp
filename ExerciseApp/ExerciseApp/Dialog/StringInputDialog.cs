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

namespace ExerciseApp.Dialog
{
    public class StringInputDialog : DialogFragment
    {
        public static StringInputDialog NewInstance(Bundle bundle)
        {
            var fragment = new StringInputDialog();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.StringInputDialog, container, false);

            GetUiElements();

            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            Dialog.SetCanceledOnTouchOutside(false);

            return view;
        }

        private void GetUiElements()
        {

        }
    }
}