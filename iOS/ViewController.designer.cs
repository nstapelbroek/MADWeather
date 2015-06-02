// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MADWeather.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton FindWeatherButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton FindWeatherGPSButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField LocationText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TemperatureLabel { get; set; }

		[Action ("FindWeatherButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void FindWeatherButton_TouchUpInside (UIButton sender);

		[Action ("FindWeatherGPSButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void FindWeatherGPSButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (FindWeatherButton != null) {
				FindWeatherButton.Dispose ();
				FindWeatherButton = null;
			}
			if (FindWeatherGPSButton != null) {
				FindWeatherGPSButton.Dispose ();
				FindWeatherGPSButton = null;
			}
			if (LocationText != null) {
				LocationText.Dispose ();
				LocationText = null;
			}
			if (TemperatureLabel != null) {
				TemperatureLabel.Dispose ();
				TemperatureLabel = null;
			}
		}
	}
}
