using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace LiveDex {
    public partial class PokedexPage : ContentPage {

        bool isSubdex;
        bool obtainedCheck;
        IEnumerable<DexEntry> filteredDex;

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
            Analytics.TrackEvent("Pokemon tapped");
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        void UpdateListView(object sender, System.EventArgs e) {
            var selectedItem = GenFilter.SelectedItem as PokeData.GenerationModel;
            if (!isSubdex) {
                filteredDex = PokeData.NationalDex.Where(
                    p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd);

                Count.Text = "Pokemon Count: " + (selectedItem.DexEnd - selectedItem.DexStart + 1);

            } else {
                filteredDex = PokeData.NationalDex.Where(
                    p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd && p.Obtained == obtainedCheck);

                Count.Text = filteredDex.Count() + " of " + (selectedItem.DexEnd - selectedItem.DexStart + 1);
            }
            FilterBySearch(null, null);
            PokedexList.IsRefreshing = false;
        }

        void FilterBySearch(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            var search = searchPokemon.Text;
            if (string.IsNullOrEmpty(search)) {
                PokedexList.ItemsSource = filteredDex;
            } else {
                PokedexList.ItemsSource = filteredDex.Where(mon => {
                    return mon.Name.ToLower().StartsWith(search.ToLower()) || mon.DexNum.ToString().StartsWith(search);
                });
            }
        }
    }
}
