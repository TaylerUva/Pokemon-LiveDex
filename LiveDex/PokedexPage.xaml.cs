using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveDex.Models;

using Xamarin.Forms;

namespace LiveDex {
    public partial class PokedexPage : ContentPage {

        static Pokedex pokedex;

        public PokedexPage() {
            InitializeComponent();
            PokedexList.IsRefreshing = true;
        }

        private async Task PullPokedex() {
            pokedex = await PokeData.GetPokedexList();
            PokedexList.ItemsSource = pokedex.DexEntries;
            PokedexList.IsRefreshing = false;
        }

        async void PokemonTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var pokemon = e.Item as DexEntry;
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            await PullPokedex();
        }
    }
}
