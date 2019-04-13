using System;
using System.Collections.Generic;
using LiveDex.Models;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokemonPage : ContentPage {

        Pokemon pokemon;

        public PokemonPage(DexEntry pkm) {
            InitializeComponent();
            Title = pkm.Name;
            PullPokemon(pkm);
        }

        private async void PullPokemon(DexEntry pkm) {
            pokemon = await PokeData.GetPokemon(pkm.DexNum);
            this.BindingContext = pokemon;
        }
    }
}
