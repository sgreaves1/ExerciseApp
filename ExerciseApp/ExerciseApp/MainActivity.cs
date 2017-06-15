using Android.App;
using Android.Widget;
using Android.OS;

namespace ExerciseApp
{
    [Activity(Label = "ExerciseApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get the UI controls from the loaded layout
            EditText pushUpsToAdd = FindViewById<EditText>(Resource.Id.pushUpsToAdd);
            Button addButton = FindViewById<Button>(Resource.Id.addButton);
        }
    }
}

