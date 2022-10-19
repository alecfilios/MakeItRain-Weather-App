using System;
using System.Collections.Generic;

namespace MakeItRain.Models
{
    /*
     * This class is the Model for the MVVM Architecture.
     * It holds all the necessary classes that are going to be used to structure
     * the root class that is the representation of the json data.
     */
    
    public class WeatherInfo
    {
        public class coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class weather
        {
            public double id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set;  }
            public double pressure { get; set; }
            public double humidity { get; set; }

        }

        public class wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class clouds
        {
            public double all { get; set; }
        }

        public class sys
        {
            public double type { get; set; }
            public double id { get; set; }
            public string country { get; set; }
            public double sunrise { get; set; }
            public double sunset { get; set; }

        }
        /* This class is the actual reflection of our data.
         * Its properties are other classes that technically represent
         * the format of the json that is going to be retrieved from the OpenWeatherAPI.
         */
        public class root
        {
            public coord coord { get; set; }
            public List<weather> weather { get; set; }
            public main main { get; set; }
            public double visibility { get; set; }
            public wind wind { get; set; }
            public double dt { get; set; }
            public clouds clouds { get; set; }
            public sys sys { get; set; }

            public double timezone { get; set; }
            public double id { get; set; }
            public string name { get; set; }
            public double cod { get; set; }
        }
    }
}

/*
 * Made by Alexandros Filios
 */