using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WPFWeather.Models;

namespace WPFWeather.Helpers
{
    public class AccuweatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_URL = "/locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CONDITIONS_URL = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "bS3ob4L4Pc4dYlMVmtGZz8A0ZT9t28M6";

        public static async Task<List<City>> GetCitiesAsync(string query)
        {
            List<City> cities = new List<City>();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_URL, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);

            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string city)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_URL, city, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
