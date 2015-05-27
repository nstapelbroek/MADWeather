using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MADWeather.Droid
{
	[Activity (Label = "MADWeather.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button findWeatherButton = FindViewById<Button> (Resource.Id.FindWeatherButton);
			findWeatherButton.Click += delegate {
				updateWeather();
			};
		}

		async void updateWeather() {
			EditText locationText = FindViewById<EditText> (Resource.Id.LocationText);
			var location = locationText.Text;
			if (!String.IsNullOrEmpty (location)) {
				Weather weather = await WeatherStation.FetchWeatherAsync (location);
				TextView temperatureLabel = FindViewById<TextView> (Resource.Id.TemperatureLabel);
				temperatureLabel.Text = String.Format("{0:0.##}°C", weather.Temperature);
			}
		}
	}
}


