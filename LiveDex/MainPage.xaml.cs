using System;
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
            pullingData.IsRunning = true;
        }

        async void LoadPokedexPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(new PokedexPage());
            } else await PullData();
        }

        async void LoadCaughtPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(new PokedexPage(true));
            } else await PullData();
        }

        async void LoadMissingPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(new PokedexPage(false));
            } else await PullData();
        }

        async void LoadSettingsPage(object sender, System.EventArgs e) {
            if (await DonePulling()) {
                await Navigation.PushAsync(new SettingsPage());
            } else await PullData();
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet Connection", "Our data is pulled from the web.\nPlease connect to the internet.", "Okay");
                return false;
            }
            return true;
        }

        private async Task<bool> DonePulling() {
            if (pullingData.IsRunning) {
                await DisplayAlert("Pokedex Still Loading", "Pokedex data is still being downloaded. Please wait until the activity indicator disappears.", "Okay");
                return false;
            }
            return true;
        }

        async Task PullData() {
            if (await HasInternet() && pullingData.IsRunning) {
                PokeData.NationalDex = await PokeData.GetPokedexList();
                await App.CaughtDatabaseInstance.PopulateDatabase(false);
                pullingData.IsRunning = false;
            }
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            await PullData();
        }
    }
}
