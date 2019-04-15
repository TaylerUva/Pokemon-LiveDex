using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokedexPage : ContentPage {

        List<DexEntry> pokedexEntries;

        public PokedexPage() {
            InitializeComponent();
            PokedexList.IsRefreshing = true;

            List<string> generationNames = new List<string>();
            generationNames.Add("All Generations");
            foreach (var gen in PokeData.Generations) {
                generationNames.Add(gen.Name + " - " + gen.Region + ": " + gen.DexStart + "-" + gen.DexEnd);
            }

            GenFilter.ItemsSource = generationNames;
        }

        private async Task PullPokedex() {
            if (await HasInternet()) {
                pokedexEntries = (await PokeData.GetPokedexList()).DexEntries;
                PokedexList.ItemsSource = pokedexEntries;
                DexCount.Text = "Dex Count: " + PokeData.MAX_DEX_NUM;
                PokedexList.IsRefreshing = false;
            }
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

        void FilterChanged(object sender, System.EventArgs e) {
            PokedexList.ItemsSource = pokedexEntries.Where(p => p.DexNum >= CheckFilter().DexStart && p.DexNum <= CheckFilter().DexEnd);
        }

        PokeData.GenerationModel CheckFilter() {
            foreach (var gen in PokeData.Generations) {
                if (GenFilter.SelectedItem.ToString().Contains(gen.Region)) return gen;
            }
            return new PokeData.GenerationModel { Name = "All", DexStart = PokeData.MIN_DEX_NUM, DexEnd = PokeData.MAX_DEX_NUM };

        }
    }
}
