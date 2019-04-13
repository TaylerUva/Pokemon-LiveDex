using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveDex.Models;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokemonPage : ContentPage {

        Pokemon pokemon;
        DexEntry dexEntry;

        public PokemonPage(DexEntry pkm) {
            InitializeComponent();
            loadingIcon.IsRunning = true;
            Title = pkm.Name;
            dexEntry = pkm;
        }

        private async Task PullPokemon(DexEntry pkm) {
            pokemon = await PokeData.GetPokemon(pkm.DexNum);
            pkmCaught.IsToggled = pokemon.caught;
            pkmSprite.Source = pkm.Sprite;
            string type1 = pokemon.Types[0].Type.Name;
            BackgroundColor = Color.FromHex(PokeData.GetTypeColor(type1));
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e) {
            PokeData.CaughtPokemonList()[pokemon.Id] = e.Value;
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            await PullPokemon(dexEntry);
            loadingIcon.IsRunning = false;
        }
    }
}
