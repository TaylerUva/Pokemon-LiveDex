﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokedexPage : ContentPage {

        public PokedexPage() {
            InitializeComponent();
            PokedexList.IsRefreshing = true;
        }

        void Handle_Appearing(object sender, System.EventArgs e) {
            if (PokedexList.IsRefreshing == true) {
                GenFilter.ItemsSource = PokeData.Generations;
                GenFilter.SelectedIndex = 0;
                PokedexList.IsRefreshing = false;
            } else
                UpdateListView(null, null);
        }

        async void PokemonTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var pokemon = e.Item as DexEntry;
            await Navigation.PushAsync(new PokemonPage(pokemon));
        }

        void UpdateListView(object sender, System.EventArgs e) {
            var selectedItem = GenFilter.SelectedItem as PokeData.GenerationModel;
            DexCount.Text = "Dex Count: " + (selectedItem.DexEnd - selectedItem.DexStart + 1);
            PokedexList.ItemsSource = PokeData.NationalDex.Where(
                p => p.DexNum >= selectedItem.DexStart && p.DexNum <= selectedItem.DexEnd);
            PokedexList.IsRefreshing = false;
        }
    }
}
