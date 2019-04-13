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
            loadingIcon.IsRunning = true;
            PullPokemon(pkm);
            loadingIcon.IsRunning = false;
        }

        private async void PullPokemon(DexEntry pkm) {
            pokemon = await PokeData.GetPokemon(pkm.DexNum);
            pkmSprite.Source = pkm.Sprite;
            string type1 = pokemon.Types[0].Type.Name;
            BackgroundColor = Color.FromHex(PokeData.GetTypeColor(type1));
        }
    }
}
