using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokedexPage : ContentPage {

        bool pulledPreviously;

        List<DexEntry> pokedexEntries;

        public PokedexPage() {
            InitializeComponent();
            PokedexList.IsRefreshing = true;

            GenFilter.ItemsSource = PokeData.Generations;
        }

        private async Task PullPokedex() {
            if (await HasInternet() && !pulledPreviously) {
                pokedexEntries = (await PokeData.GetPokedexList()).DexEntries;
                PokedexList.ItemsSource = pokedexEntries;
                DexCount.Text = "Dex Count: " + PokeData.MAX_DEX_NUM;
                PokedexList.IsRefreshing = false;
                pulledPreviously = true;
            }
        }

        async void PokemonTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var pokemon = e.Item as DexEntry;
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            if (pulledPreviously) UpdateListView();
            else await PullPokedex();
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet", "Please connect to the internet", "Close");
                return false;
            }
            return true;
        }

        void FilterChanged(object sender, System.EventArgs e) {
            UpdateListView();
        }

        void UpdateListView() {
            var selectedItem = GenFilter.SelectedItem as PokeData.GenerationModel;
            if (selectedItem == null) {
                PokedexList.ItemsSource = pokedexEntries.Where(
                    p => p.DexNum >= PokeData.AllGens.DexStart && p.DexNum <= PokeData.AllGens.DexEnd);
                System.Diagnostics.Debug.WriteLine("CHANGED");
            } else {
                DexCount.Text = "Dex Count: " + (selectedItem.DexEnd - selectedItem.DexStart + 1);
                PokedexList.ItemsSource = pokedexEntries.Where(
                    p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd);
            }
        }
    }
}
