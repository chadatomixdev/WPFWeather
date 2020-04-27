using System.Collections.ObjectModel;
using System.ComponentModel;
using WPFWeather.Commands;
using WPFWeather.Helpers;
using WPFWeather.Models;

namespace WPFWeather.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public WeatherViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new City() { LocalizedName = "New York" };
                currentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly Cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        public ObservableCollection<City> Cities { get; set; }

        public SearchCommand SearchCommand { get; set; }

        private string query;

        public string Query
        {
            get { return query; }
            set {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditionsAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQueryAsync()
        {
            var cities = await AccuweatherHelper.GetCitiesAsync(Query);

            Cities.Clear();

            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditionsAsync()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuweatherHelper.GetCurrentConditionsAsync(SelectedCity.Key);
        }
    }
}