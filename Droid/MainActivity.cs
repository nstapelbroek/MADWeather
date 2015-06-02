﻿using System;

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

		TextView temperatureLabel;

		Button findWeatherByLocationButton;

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

			findWeatherByLocationButton = FindViewById<Button> (Resource.Id.FindWeatherByLocationButton);
			findWeatherByLocationButton.Click += delegate {
				startLocationProvider ();
				findWeatherByLocationButton.Visibility = ViewStates.Gone;
			};

			locationManager = GetSystemService (Context.LocationService) as LocationManager;
			temperatureLabel = FindViewById<TextView> (Resource.Id.TemperatureLabel);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			startLocationProvider ();
		}

		protected void startLocationProvider ()
		{
			string Provider = LocationManager.GpsProvider;
			if (locationManager.AllProviders.Contains (LocationManager.NetworkProvider)
			    && locationManager.IsProviderEnabled (LocationManager.NetworkProvider)) {
				locationManager.RequestLocationUpdates (LocationManager.NetworkProvider, 0, 0, this);
				Log.Info ("location", "Requesting locationupdates for provider: " + Provider);
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
				temperatureLabel.Text = String.Format ("{0:0.##}°C", weather.Temperature);
			}
		}

		async void updateWeather (Location location)
		{
			Weather weather = await WeatherStation.FetchWeatherAsync (location.Latitude, location.Longitude);
			temperatureLabel.Text = String.Format ("{0:0.##}°C", weather.Temperature);
			locationManager.RemoveUpdates (this);
			findWeatherByLocationButton.Visibility = ViewStates.Visible;
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


