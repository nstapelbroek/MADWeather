using System;

using UIKit;
using CoreLocation;
using Foundation;
using System.Threading.Tasks;

namespace MADWeather.iOS
{
	public partial class ViewController : UIViewController, ICLLocationManagerDelegate
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			LocationText.ShouldReturn += (UITextField textField) => {
				GetWeather (textField.Text);
				textField.ResignFirstResponder ();
				return true;
			};
		}

		async partial void FindWeatherButton_TouchUpInside (UIButton sender)
		{
			await GetWeather (LocationText.Text);
		}

		async Task GetWeather (string city)
		{
			if (string.IsNullOrWhiteSpace (city)) {
				showAlert ("Error", "Please enter a cityname");
				return;
			}

			showLoadingAlert ();

			Weather weather = await WeatherStation.FetchWeatherAsync (city);
			TemperatureLabel.Text = string.Format ("{0:F1} °C", weather.Temperature);

			dontShowLoadingAlert ();
		}

		void showAlert (string title, string message)
		{
			var alert = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
			PresentViewController (alert, true, null);
		}

		UIAlertController loadingAlert;
		bool showingLoadingALert = false;
		void showLoadingAlert ()
		{
			showingLoadingALert = true;
			loadingAlert = UIAlertController.Create ("Getting location", " ", UIAlertControllerStyle.ActionSheet);
			UIActivityIndicatorView indicator = new UIActivityIndicatorView (
				new CoreGraphics.CGRect (
					new CoreGraphics.CGPoint (
						loadingAlert.View.Bounds.Location.X, 
						loadingAlert.View.Bounds.Location.Y - 8),
					loadingAlert.View.Bounds.Size)
			){
				ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight,
				UserInteractionEnabled = false
			};
			indicator.StartAnimating ();
			loadingAlert.Add (indicator);
			loadingAlert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Destructive, null));
			PresentViewController (loadingAlert, true, null);
		}

		void dontShowLoadingAlert ()
		{
			if (!showingLoadingALert)
				return;
			loadingAlert.DismissViewController (true, null);
		}

		CLLocationManager locationManager;

		partial void FindWeatherGPSButton_TouchUpInside (UIButton sender)
		{
			showLoadingAlert ();
			locationManager = new CLLocationManager ();

			if (locationManager.RespondsToSelector (new ObjCRuntime.Selector ("requestWhenInUseAuthorization")))
				locationManager.RequestWhenInUseAuthorization ();

			locationManager.DistanceFilter = CLLocationDistance.FilterNone;
			locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
			locationManager.Delegate = (ICLLocationManagerDelegate)Self;

			if (CLLocationManager.LocationServicesEnabled)
				locationManager.StartUpdatingLocation ();
		}

		[Export ("locationManager:didUpdateLocations:")]
		async void LocationsUpdated (CoreLocation.CLLocationManager manager, CoreLocation.CLLocation[] locations)
		{
			locationManager.StopUpdatingLocation ();

			CLLocation location = locations [0];
			double lat = location.Coordinate.Latitude;
			double lon = location.Coordinate.Longitude;

			Weather weather = await WeatherStation.FetchWeatherAsync (lat, lon);
			TemperatureLabel.Text = string.Format ("{0:F1} °C", weather.Temperature);

			dontShowLoadingAlert ();
		}
	}
}
