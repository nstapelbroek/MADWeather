﻿using System;

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
			Button countClickButton = FindViewById<Button> (Resource.Id.CountClickButton);
			
			countClickButton.Click += delegate {
				countClickButton.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}


