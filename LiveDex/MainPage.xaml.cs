﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MainPage : ContentPage {


        public MainPage() {
            InitializeComponent();
        }

        async void LoadPokedex(object sender, System.EventArgs e) {
            if (await HasInternet()) {
                await Navigation.PushAsync(new PokedexPage());
            }
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet Connection", "Our data is pulled from the web, please connect to the internet", "Close");
                return false;
            }
            return true;
        }
    }
}
