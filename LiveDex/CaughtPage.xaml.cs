using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class CaughtPage : ContentPage {

        public CaughtPage() {
            InitializeComponent();
            PokedexList.IsRefreshing = true;
        }

        private async Task PullPokedex() {
            if (await HasInternet()) {
                var subdex = await GetSubDex();
                CaughtCount.Text = "Caught: " + subdex.Count + " of " + PokeData.MAX_DEX_NUM;
                PokedexList.ItemsSource = subdex;
                PokedexList.IsRefreshing = false;
            }
        }

        private async Task<List<DexEntry>> GetSubDex() {
            return (await PokeData.GetPokedexList()).DexEntries.FindAll((obj) => obj.Obtained.Equals(true));
        }

        async void PokemonTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var pokemon = e.Item as DexEntry;
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            await PullPokedex();
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet", "Please connect to the internet", "Close");
                return false;
            }
            return true;
        }
    }
}
