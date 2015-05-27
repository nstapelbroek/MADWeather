using System;
		
using UIKit;

namespace MADWeather.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			FindWeatherButton.TouchUpInside += delegate {
				updateWeather ();
			};
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}

		async void updateWeather ()
		{
			var location = LocationText.Text;
			if (!String.IsNullOrEmpty (location)) {
				Weather weather = await WeatherStation.FetchWeatherAsync (location);
				TemperatureLabel.Text = String.Format ("{0:0.##}°C", weather.Temperature);
			}
		}
	}
}
