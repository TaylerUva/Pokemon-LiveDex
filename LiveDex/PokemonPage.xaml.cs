using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace LiveDex {
    public partial class PokemonPage : ContentPage {

        Pokemon pokemon;
        DexEntry dexEntry;

        bool pulledPreviously;

        public PokemonPage(DexEntry pkm) {
            InitializeComponent();
            loadingIcon.IsRunning = true;
            LocationList.IsRefreshing = true;
            Title = pkm.Name;
            dexEntry = pkm;
        }

        private async Task PullPokemonDetails() {
            pkmSprite.Source = dexEntry.Sprite;
            pkmCaught.IsToggled = dexEntry.Obtained;
            if (!pulledPreviously && await HasInternet()) {
                pokemon = await PokeData.GetPokemon(dexEntry.DexNum);
                string type1 = pokemon.Types[0].Type.Name;
                BackgroundColor = Color.FromHex(PokeData.GetTypeColor(type1));

                var gamesCatchable = pokemon.GamesCatchable;
                gamesCatchable.Add("All Games");
                gamesCatchable.Sort();

                GameFilter.ItemsSource = gamesCatchable;
                LocationList.ItemsSource = pokemon.Routes;
                pulledPreviously = true;
            }
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e) {
            dexEntry.Obtained = e.Value;
            if (dexEntry.Obtained){
                string sendEvent = dexEntry.DexNum.ToString("000") + " - " + dexEntry.Name + " - Caught";
                Analytics.TrackEvent(sendEvent);
            }
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            loadingIcon.IsRunning = true;
            await PullPokemonDetails();
            loadingIcon.IsRunning = false;
            LocationList.IsRefreshing = false;
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet!", "Unable to get additional data!\nCaught status can still be set when offline.", "Okay");
                return false;
            }
            return true;
        }

        async void RouteTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var route = e.Item as Route;
            if (route.Details != null) {
                await Navigation.PushAsync(new RoutePage(route));
            }
        }

        void FilterChanged(object sender, System.EventArgs e) {
            string selectedFilter = GameFilter.SelectedItem.ToString();
            if (selectedFilter.Equals("All Games")) LocationList.ItemsSource = pokemon.Routes;
            else LocationList.ItemsSource = pokemon.Routes.Where(r => r.Details.Version.FormattedName.Equals(selectedFilter));
        }
    }
}
