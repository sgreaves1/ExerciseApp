using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Net.Http;
using Android.Graphics;
using Android.Net;
using Android.Support.V7.App;
using Android.Views;
using ExerciseApp.Model;
using ExerciseApp.Data;
using ExerciseApp.Eunmerators;
using Felipecsl.GifImageViewLibrary;

namespace ExerciseApp
{
    [Activity(Label = "AddExerciseActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class AddExerciseActivity : AppCompatActivity, GifImageView.IOnFrameAvailableListener
    {
        private readonly Database _db = new Database("exercise.db3");

        private TextView _exerciseNameLabel;
        private EditText _amount;
        private EditText _weight;
        private Spinner _weightTypeSpinner;
        private Button _okButton;
        private Button _cancelButton;
        private TextView _dataWarning;
        private Button _showButton;
        private GifImageView _gifImage;

        private int _routineId;
        private WeightUnits _weightUnit = WeightUnits.None;
        private string _gifLocation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "AddExercise" layout resource
            SetContentView(Resource.Layout.AddExercise);

            // Get the UI controls from the loaded layout
            GetUiElements();

            // Create your application here
            _exerciseNameLabel.Text = Intent.GetStringExtra("ExerciseName") ?? "Unknown Exercise";
            _gifLocation = Intent.GetStringExtra("ExerciseGif") ?? "";
            _routineId = Intent.GetIntExtra("RoutineId", 0);

            ConnectivityManager connectivityManager = (ConnectivityManager) GetSystemService(ConnectivityService);

            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;

            if (networkInfo.IsConnected)
            {
                if (networkInfo.Type == ConnectivityType.Wifi)
                {
                    GetGif();
                }
                else if (networkInfo.Type == ConnectivityType.Mobile)
                {
                    _dataWarning.Visibility = ViewStates.Visible;
                    _showButton.Visibility = ViewStates.Visible;
                }
            }
            else
            {
                
            }
        }
        
        private void GetUiElements()
        {
            _exerciseNameLabel = FindViewById<TextView>(Resource.Id.ExerciseName);
            _amount = FindViewById<EditText>(Resource.Id.exerciseAmount);
            _weight = FindViewById<EditText>(Resource.Id.exerciseWeight);
            _weightTypeSpinner = FindViewById<Spinner>(Resource.Id.weightSpinner);
            _okButton = FindViewById<Button>(Resource.Id.okButton);
            _cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            _dataWarning = FindViewById<TextView>(Resource.Id.warningLabel);
            _showButton = FindViewById<Button>(Resource.Id.showButton);
            _gifImage = FindViewById<GifImageView>(Resource.Id.gifImageView);
            
            // event handlers
            _weightTypeSpinner.ItemSelected += WeightTypeSpinnerItemSelected;
            _okButton.Click += OkButtonOnClick;
            _cancelButton.Click += CancelButtonOnClick;
            _showButton.Click += ShowButtonOnClick;

            // Spinner adapter
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.weight_types,
                Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _weightTypeSpinner.Adapter = adapter;

            _gifImage.OnFrameAvailableListener = this;

            _dataWarning.Visibility = ViewStates.Invisible;
            _showButton.Visibility = ViewStates.Invisible;
        }

        private void ShowButtonOnClick(object sender, EventArgs eventArgs)
        {
            _showButton.Visibility = ViewStates.Invisible;
            _dataWarning.Visibility = ViewStates.Invisible;
            GetGif();
        }

        private void WeightTypeSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
        {
            _weightUnit = WeightUnitsHelper.FromString(_weightTypeSpinner.GetItemAtPosition(itemSelectedEventArgs.Position).ToString());
        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {
            try
            {
                if (int.Parse(_amount.Text) > 0)
                {
                    Exercise exercise = new Exercise
                    {
                        RoutineId = _routineId,
                        Name = _exerciseNameLabel.Text,
                        Date = DateTime.Now,
                        Amount = int.Parse(_amount.Text),
                        Weight = double.Parse(_weight.Text),
                        WeightUnits = _weightUnit,
                        Done = true,
                    };

                    _db.InsertData(exercise);

                    Toast.MakeText(this, exercise.Name + " added to today's routine!", ToastLength.Short).Show();

                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Amount must be greater than 0!", ToastLength.Short).Show();
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Invalid Input! Amount should be numbers only!", ToastLength.Short).Show();
            }
        }
        
        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }

        public Bitmap OnFrameAvailable(Bitmap bitmap)
        {
            return bitmap;
        }

        public async void GetGif()
        {
            using (var client = new HttpClient())
            {
                var bytes = await client.GetByteArrayAsync(_gifLocation);
                _gifImage.SetBytes(bytes);
                _gifImage.StartAnimation();
            }
        }
    }
}