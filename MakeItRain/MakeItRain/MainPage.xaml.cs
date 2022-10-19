using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MakeItRain.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MakeItRain
{
    /*
     * Because I followed the MVVM architecture this class is as clean as possible.
     */
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BindingContext = new MainViewModel();
        }      
    }
}


/*
 * Made by Alexandros Filios
 */
