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
            var pokedex = await PokeData.GetPokedexList();
            ToCatchList.ItemsSource = pokedex.Entries;
        }
    }
}
