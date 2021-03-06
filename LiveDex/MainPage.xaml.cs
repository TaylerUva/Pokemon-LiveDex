﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace LiveDex {
    public partial class MainPage : ContentPage {

        bool loadingData = true;

        PokedexPage FullDex;
        PokedexPage CaughtDex;
        PokedexPage MissingDex;

        public MainPage() {
            InitializeComponent();
            pullingData.IsRunning = true;
        }

        async void LoadPokedexPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(FullDex);
            } else await PullData();
        }

        async void LoadCaughtPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(CaughtDex);
            } else await PullData();
        }

        async void LoadMissingPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(MissingDex);
            } else await PullData();
        }

        async void LoadSettingsPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(new SettingsPage());
            } else await PullData();
        }

        private async Task<bool> HasInternet() {
            try {
                if (!CrossConnectivity.Current.IsConnected) {
                    await DisplayAlert("No Internet Connection", "Our data is pulled from the web.\nPlease connect to the internet.", "Okay");
                    throw new Exception("No internet connection");
                }
            } catch (Exception e) {
                Crashes.TrackError(e);
                return false;
            }
            return true;
        }

        private async Task<bool> DonePulling() {
            if (loadingData) {
                await DisplayAlert("Pokedex Still Loading", "Pokedex data is still being downloaded. Please wait until the activity indicator disappears.", "Okay");
                return false;
            }
            return true;
        }

        async Task PullData() {
            if (loadingData && await HasInternet()) {
                PokeData.NationalDex = await PokeData.GetPokedexList();
                await App.CaughtDatabaseInstance.PopulateDatabase(false);

                FullDex = new PokedexPage();
                CaughtDex = new PokedexPage(true);
                MissingDex = new PokedexPage(false);

                pullingData.IsRunning = false;
                loadingData = false;
            }
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            await PullData();
            if (!loadingData) {
                if (PokeData.AllCaught()) {
                    catchButton.IsEnabled = true;
                    releaseButton.IsEnabled = false;
                } else if (PokeData.AllMissing()) {
                    releaseButton.IsEnabled = true;
                    catchButton.IsEnabled = false;
                } else {
                    catchButton.IsEnabled = true;
                    releaseButton.IsEnabled = true;
                }
            }
        }

        async void Handle_SearchButtonPressed(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                var searchText = searchPokemon.Text.ToLower();
                var pokemon = PokeData.NationalDex.Find((mon) => mon.Name.ToLower() == searchText || mon.DexNum.ToString() == searchText);
                if (pokemon == null) await DisplayAlert("Pokemon Not Found", "Pokemon Name or ID does not exist", "Okay");
                else await Navigation.PushAsync(new PokemonPage(pokemon));
            } else await PullData();
        }
    }
}
