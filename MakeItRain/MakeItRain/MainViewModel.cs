using System;
using Newtonsoft.Json;
using System.Net;
using MakeItRain.Models;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItRain
{
    /*
     * This class is the View Model for the MVVM Architecture
     */
    public class MainViewModel : INotifyPropertyChanged
    {
        /* 
        * ----------------------------------------------------------------------
        *                          Properties
        * ----------------------------------------------------------------------
        */
        // The key needed to make the API calls.
        string APIKey = "4b4e5f42be6f902a44cbef396380ea08";

        // In order to implement the interface and update the view's values
        public event PropertyChangedEventHandler PropertyChanged;

        // It could be static or use the singleton design pattern, but it was not needed.
        WeatherInfo.root Info = new WeatherInfo.root();

        // The command for the button to follow
        public Command UpdateWeatherInfoCommand { get; set; }

        // The properties used for the binding
        public string CityNameEntryValue { get; set; }
        public string IconSourceText { get; set; }
        public string CityLabelText { get; set;}
        public string TimezoneLabelText { get; set; }
        public string LatitudeLabelText { get; set; }
        public string LongitudeLabelText { get; set; }
        public string WeatherLabelText { get; set; }
        public string DescriptionLabelText { get; set; }
        public string TemperatureLabelText { get; set; }
        public string FeelsLikeLabelText { get; set; }
        public string MinLabelText { get; set; }
        public string MaxLabelText { get; set; }
        public string PressureLabelText { get; set; }
        public string HumidityLabelText { get; set; }
        public string SpeedLabelText { get; set; }
        public string DegreeLabelText { get; set; }
        public string SunriseLabelText { get; set; }
        public string SunsetLabelText { get; set; }

        /* 
        * ----------------------------------------------------------------------
        *                          Constructor
        * ----------------------------------------------------------------------
        */
        public MainViewModel()
        {
            // Put the N/A value to all the properties on the View
            ClearValues();
            // Trigegr the changes
            OnPropertyChangeTrigger();
            // Set the command for the button
            UpdateWeatherInfoCommand = new Command(() =>
            {
                UpdateWeatherInfoExecute();
            });
        }
        /* 
        * ----------------------------------------------------------------------
        *                          Methods
        * ----------------------------------------------------------------------
        */

        /* 
         * This method uses one background thread from the trheadpool 
         * in order for the other functionalities of the app (such as the scroll of the scrollview)
         * to not freeze if the download takes time. According to the input, 
         * the method can either fill the UI elements with the appropriate data or if the Input is incorrect,
         * to just fill the values with N/A.
         */
        public void UpdateWeatherInfoExecute()
        {
            // Set a boolean that deternines whether data wss retrieved from the API
            bool isDataLoaded = false;
            // Queues the download of data to run on the thread pool
            Task.Run(() =>
            {
                // Code to be run in a background thread
                using (WebClient web = new WebClient())
                {
                    // Try to reach the api with the given city name
                    try
                    {
                        // get the url of the specific city
                        string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", CityNameEntryValue, APIKey);
                        // format it into a json
                        var json = web.DownloadString(url);
                        // fill the Info which is the model
                        Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                        // trigger the bool that everything went smoothly
                        isDataLoaded = true;
                    }
                    catch
                    {
                        // In any other case do nothing and continue..
                    }
                    
                }
                // In any case the UI needs to be refreshed
                Device.BeginInvokeOnMainThread(() =>
                {
                    // UI interaction goes here
                    // If data was indeed retrieved, Update it.
                    if (isDataLoaded) UpdateUI();
                    // Else just put N/A to all the values
                    else ClearValues();
                    // Trigger the OnProperty change to actually refresh the UI elements
                    OnPropertyChangeTrigger();

                });
            });


            
        }
        /*
         * This method is responsible for handling and formatting the data in the appropriate way
         * for each metric to appear right on the UI.
         */
        void UpdateUI()
        {
            IconSourceText = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";

            CityLabelText = Info.name.ToString();

            TimezoneLabelText = "UTC+" + (Info.timezone / 60 / 60).ToString() + "h";

            LatitudeLabelText = Info.coord.lat.ToString();

            LongitudeLabelText = Info.coord.lon.ToString();

            WeatherLabelText = Info.weather[0].main.ToString();

            DescriptionLabelText = Info.weather[0].description.ToString();

            TemperatureLabelText = (Info.main.temp - 273.15).ToString() + "C";

            FeelsLikeLabelText = (Info.main.feels_like - 273.15).ToString() + "C";

            MinLabelText = (Info.main.temp_min - 273.15).ToString() + "C";

            MaxLabelText = (Info.main.temp_max - 273.15).ToString() + "C";

            PressureLabelText = Info.main.pressure.ToString() + "hPa";

            HumidityLabelText = Info.main.humidity.ToString() + "%";

            SpeedLabelText = Info.wind.speed.ToString() + "m/s";

            DegreeLabelText = Info.wind.deg.ToString();

            SunriseLabelText = ConvertDateTime(Info.sys.sunrise).ToShortTimeString();

            SunsetLabelText = ConvertDateTime(Info.sys.sunset).ToShortTimeString();
        }
        /*
         * This method just clears the values with the default ones.
         */
        void ClearValues()
        {
            IconSourceText = "defaultImage";
            CityLabelText = "N/A";
            TimezoneLabelText = "N/A";
            LatitudeLabelText = "N/A";
            LongitudeLabelText = "N/A";
            WeatherLabelText = "N/A";
            DescriptionLabelText = "N/A";
            TemperatureLabelText = "N/A";
            FeelsLikeLabelText = "N/A";
            MinLabelText = "N/A";
            MaxLabelText = "N/A";
            PressureLabelText = "N/A";
            HumidityLabelText = "N/A";
            SpeedLabelText = "N/A";
            DegreeLabelText = "N/A";
            SunriseLabelText = "N/A";
            SunsetLabelText = "N/A";
        }
        /*
         * This method is responsible for the Binding and makes the changes appear on the View.
         * It is basically what makes the MVVM.
         */
        void OnPropertyChangeTrigger()
        {
            OnPropertyChanged(nameof(IconSourceText));
            OnPropertyChanged(nameof(CityLabelText));
            OnPropertyChanged(nameof(TimezoneLabelText));
            OnPropertyChanged(nameof(LatitudeLabelText));
            OnPropertyChanged(nameof(LongitudeLabelText));
            OnPropertyChanged(nameof(WeatherLabelText));
            OnPropertyChanged(nameof(DescriptionLabelText));
            OnPropertyChanged(nameof(TemperatureLabelText));
            OnPropertyChanged(nameof(FeelsLikeLabelText));
            OnPropertyChanged(nameof(MinLabelText));
            OnPropertyChanged(nameof(MaxLabelText));
            OnPropertyChanged(nameof(PressureLabelText));
            OnPropertyChanged(nameof(HumidityLabelText));
            OnPropertyChanged(nameof(SpeedLabelText));
            OnPropertyChanged(nameof(DegreeLabelText));
            OnPropertyChanged(nameof(SunriseLabelText));
            OnPropertyChanged(nameof(SunsetLabelText));
        }
        /*
         * This function is a utility 
         * that changes the milliseconds into a date, 
         * since January 1st 1970. 
         */
        DateTime ConvertDateTime(double millisec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddMilliseconds(millisec).ToLocalTime();
            return day;
        }
        /*
         * This method takes the name of a property and triggers the binding for that property.
         */
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

/*
 * Made by Alexandros Filios
 */