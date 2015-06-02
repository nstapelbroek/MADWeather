using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;

namespace MADWeather.Droid
{
	[Activity (Label = "MADWeather.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener
	{
		LocationManager locationManager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button findWeatherButton = FindViewById<Button> (Resource.Id.FindWeatherButton);
			findWeatherButton.Click += delegate {
				updateWeather ();
			};

			this.locationManager = GetSystemService (Context.LocationService) as LocationManager;
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			string Provider = LocationManager.GpsProvider;

			if (locationManager.AllProviders.Contains (LocationManager.NetworkProvider)
				&& locationManager.IsProviderEnabled (LocationManager.NetworkProvider)) {
				locationManager.RequestLocationUpdates (LocationManager.NetworkProvider, 0, 0, this);
			} else {
				Log.Error ("location", Provider + " is not available. Does the device have location services enabled?");
			}
		}

		async void updateWeather ()
		{
			EditText locationText = FindViewById<EditText> (Resource.Id.LocationText);
			var location = locationText.Text;
			if (!String.IsNullOrEmpty (location)) {
				Weather weather = await WeatherStation.FetchWeatherAsync (location);
				TextView temperatureLabel = FindViewById<TextView> (Resource.Id.TemperatureLabel);
				temperatureLabel.Text = String.Format ("{0:0.##}°C", weather.Temperature);
			}
		}

		async void updateWeather (Location location)
		{
			Weather weather = await WeatherStation.FetchWeatherAsync (location.Latitude, location.Longitude);
			TextView temperatureLabel = FindViewById<TextView> (Resource.Id.TemperatureLabel);
			temperatureLabel.Text = String.Format ("{0:0.##}°C", weather.Temperature);
		}

		public void OnLocationChanged (Location location)
		{
			Log.Info ("location", String.Format ("got location, lat: {0}, long: {1}", location.Latitude, location.Longitude));
			updateWeather (location);
		}

		public void OnProviderDisabled (string provider)
		{
			// throw new NotImplementedException ();
		}

		public void OnProviderEnabled (string provider)
		{
			// throw new NotImplementedException ();
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			// throw new NotImplementedException ();
		}

	}
}


