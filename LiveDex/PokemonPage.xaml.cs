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
            if (!pulledPreviously && await HasInternet()) {
                pokemon = await PokeData.GetPokemon(dexEntry.DexNum);
                string type1 = pokemon.Types[0].Type.Name;

                var gamesCatchable = pokemon.GamesCatchable;
                gamesCatchable.Add("All Games");
                gamesCatchable.Sort();

                GameFilter.ItemsSource = gamesCatchable;
                LocationList.ItemsSource = pokemon.Routes;
                pulledPreviously = true;


                SetBadges();
            }
        }

        void SetBadges() {
            string types = pokemon.Types[0].Type.Name;
            if (pokemon.Types.Count == 2) {
                types = types + "," + pokemon.Types[1].Type.Name;
                Type2.IsEnabled = true;
            }
            string[] badges = PokeData.GetTypeBadge(types).Split(',');
            Type1.Source = badges[0];
            if (badges.Count() == 2) {
                Type1.Source = badges[1];
                Type2.Source = badges[0];
            } else {
                Type1.Source = badges[0];
            }
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            loadingIcon.IsRunning = true;
            await PullPokemonDetails();
            loadingIcon.IsRunning = false;
            LocationList.IsRefreshing = false;
            SetCaughtIcon();
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

        void SetCaughtIcon() {
            if (dexEntry.Obtained) {
                caughtLabel.Text = "Caught";
                pokeBall.Source = "pokeball.png";
            } else {
                caughtLabel.Text = "Not Caught";
                pokeBall.Source = "missing.png";
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            dexEntry.Obtained = !dexEntry.Obtained;
            if (dexEntry.Obtained && pulledPreviously) {
                string sendEvent = dexEntry.DexNum.ToString("000") + " - " + dexEntry.Name + " - Caught";
                Analytics.TrackEvent(sendEvent);
            }
            SetCaughtIcon();
        }
    }
}
