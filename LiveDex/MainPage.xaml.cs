﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveDex.Models;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MainPage : ContentPage {

        Pokemon pokedexData;


        public MainPage() {
            InitializeComponent();
        }

        async void LoadLiveDex(object sender, System.EventArgs e) {
            pokedexData = await PokeData.GetPokemon(1);
            await Navigation.PushAsync(new LiveDex());
        }
    }
}
