using System;
using System.Windows.Input;
using WPFWeather.ViewModels;

namespace WPFWeather.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherViewModel WeatherViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public SearchCommand(WeatherViewModel weatherViewModel)
        {
            WeatherViewModel = weatherViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            WeatherViewModel.MakeQueryAsync();
        }
    }
}
