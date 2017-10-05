using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Data.Json;

using System.Diagnostics;

namespace OpenWeatherMap
{
    class OpenWeatherMap
    {
        public string apiKey = null;
        public string baseURL = "http://api.openweathermap.org/data/2.5/weather?units=imperial";

        public void SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<float> GetTemperature(string zipcode)
        {
            if (this.apiKey == null)
                return 0;

            try
            {
                string url = this.baseURL;
                url += "&appid=" + this.apiKey;
                url += "&zip=" + zipcode;

                Uri uri = new Uri(url);

                HttpClient client = new HttpClient();
                string response = await client.GetStringAsync(uri);

                JsonObject json = JsonObject.Parse(response);
                var main = json["main"].GetObject();
                return float.Parse(main["temp"].ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
