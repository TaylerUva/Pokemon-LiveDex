using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokedexPage : ContentPage {

        bool isSubdex;
        bool obtainedCheck;

        public PokedexPage(bool checkIsCaught) {
            InitializeComponent();
            isSubdex = true;
            obtainedCheck = checkIsCaught;
            if (checkIsCaught) Title = "Caught";
            else Title = "Missing";

            InitialLoad();
        }

        public PokedexPage() {
            InitializeComponent();

            InitialLoad();
        }

        void InitialLoad() {
            GenFilter.ItemsSource = PokeData.Generations;
            GenFilter.SelectedIndex = 0;
            UpdateListView(null, null);
        }

        void Handle_Appearing(object sender, System.EventArgs e) {
            UpdateListView(null, null);
        }

        async void PokemonTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var pokemon = e.Item as DexEntry;
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        void UpdateListView(object sender, System.EventArgs e) {
            var selectedItem = GenFilter.SelectedItem as PokeData.GenerationModel;
            if (!isSubdex) {
                Count.Text = "Pokemon Count: " + (selectedItem.DexEnd - selectedItem.DexStart + 1);
                PokedexList.ItemsSource = PokeData.NationalDex.Where(
                    p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd);
            } else {
                var subdex = PokeData.NationalDex.Where(
                    p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd && p.Obtained == obtainedCheck);
                Count.Text = subdex.Count() + " of " + (selectedItem.DexEnd - selectedItem.DexStart + 1);
                PokedexList.ItemsSource = subdex;
            }
            PokedexList.IsRefreshing = false;
        }
    }
}
