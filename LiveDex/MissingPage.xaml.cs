using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MissingPage : ContentPage {

        bool pulledPreviously;
        List<DexEntry> pokedexEntries;

        public MissingPage() {
            InitializeComponent();
            PokedexList.IsRefreshing = true;

            GenFilter.ItemsSource = PokeData.Generations;
        }

        private async Task PullPokedex() {
            if (await HasInternet() && !pulledPreviously) {
                pokedexEntries = (await PokeData.GetPokedexList()).DexEntries;
                var subdex = pokedexEntries.Where(
                    p => p.Obtained == false);
                MissingCount.Text = "Missing: " + subdex.Count() + " of " + PokeData.MAX_DEX_NUM;
                PokedexList.ItemsSource = subdex;
                PokedexList.IsRefreshing = false;
                pulledPreviously = true;
            }
        }

        async void PokemonTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var pokemon = e.Item as DexEntry;
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet", "Please connect to the internet", "Close");
                return false;
            }
            return true;
        }



        async void Handle_Appearing(object sender, System.EventArgs e) {
            if (pulledPreviously) UpdateListView();
            else await PullPokedex();
        }

        void FilterChanged(object sender, System.EventArgs e) {
            UpdateListView();
        }

        void UpdateListView() {
            var selectedItem = GenFilter.SelectedItem as PokeData.GenerationModel;
            if (selectedItem == null) {
                PokedexList.ItemsSource = pokedexEntries.Where(
                    p => p.DexNum >= PokeData.AllGens.DexStart && p.DexNum <= PokeData.AllGens.DexEnd && p.Obtained == false);
            } else {
                var genSubdex = pokedexEntries.Where(
                    p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd && p.Obtained == false);
                PokedexList.ItemsSource = genSubdex;

                MissingCount.Text = "Missing: " + genSubdex.Count() + " of " + (selectedItem.DexEnd - selectedItem.DexStart + 1);
            }
        }
    }
}
