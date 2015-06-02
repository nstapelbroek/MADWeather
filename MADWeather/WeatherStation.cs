using System;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace MADWeather
{
	public class WeatherStation
	{
		public static async Task<Weather> FetchWeatherAsync (double latitude, double longitude)
		{
			string url = String.Format (
				"http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric"
				, latitude, longitude);
			var json = await FetchData (url);

			return parseWeatherJson (json);
		}
		public static async Task<Weather> FetchWeatherAsync (string city)
		{
			string url = String.Format (
				"http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric"
				, city);
			var json = await FetchData (url);

			return parseWeatherJson (json);
		}

		static async Task<JsonValue> FetchData (string url)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ()) {
				using (Stream stream = response.GetResponseStream ()) {
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					return jsonDoc;
				}
			}
		}

		static Weather parseWeatherJson (JsonValue json)
		{
			double rain = 0.0;
			if (json.ContainsKey ("rain")) {
				if (json ["rain"].ContainsKey ("1h"))
					rain = (double)json ["rain"] ["1h"];
				else if (json ["rain"].ContainsKey ("3h"))
					rain = (double)json ["rain"] ["3h"];
			}
			string city = json.ContainsKey ("name") ? json ["name"] : null;
			string country = json.ContainsKey ("name") ? string.Format(", {0}", json ["sys"] ["country"]) : null;

			return new Weather () {
				Date = new DateTime (1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds (json ["dt"]).ToLocalTime (),
				Location = string.Format ("{0}{1}", city, country),
				Icon = json ["weather"] [0] ["icon"],
				Rain = rain,
				Temperature = (double)json ["main"] ["temp"],
				TemperatureMax = (double)json ["main"] ["temp_max"],
				TemperatureMin = (double)json ["main"] ["temp_min"],
				WindDirection = (double)json ["wind"] ["deg"],
				WindSpeed = (double)json ["wind"] ["speed"],
			};
		}
	}
}
