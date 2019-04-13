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

        private async Task PullPokemon() {
            pokemon = await PokeData.GetPokemon(dexEntry.DexNum);
            pkmCaught.IsToggled = dexEntry.Obtained;
            pkmSprite.Source = dexEntry.Sprite;
            string type1 = pokemon.Types[0].Type.Name;
            BackgroundColor = Color.FromHex(PokeData.GetTypeColor(type1));
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e) {
            dexEntry.Obtained = e.Value;
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            loadingIcon.IsRunning = true;
            await PullPokemon();
            loadingIcon.IsRunning = false;
        }
    }
}
