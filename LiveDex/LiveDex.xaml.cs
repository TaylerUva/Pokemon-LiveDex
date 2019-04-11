using System;
using System.Collections.Generic;
using LiveDex.Models;

using Xamarin.Forms;

namespace LiveDex {
    public partial class LiveDex : ContentPage {
        public LiveDex() {
            InitializeComponent();
            PoplutateListView();
        }


        private async void PoplutateListView() {
            ToCatchList.IsRefreshing = true;
            var pokedex = await PokeData.GetPokedexList();
            ToCatchList.ItemsSource = pokedex.Entries;
            ToCatchList.IsRefreshing = false;
        }
    }
}
