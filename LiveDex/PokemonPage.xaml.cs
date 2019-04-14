﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class PokemonPage : ContentPage {

        Pokemon pokemon;
        DexEntry dexEntry;
        List<Route> routes = new List<Route>();
        List<Game> games = new List<Game>();

        public class Route {
            public string Name { get; set; }
            public VersionDetail Details { get; set; }
        }

        public class Game {
            public string Name { get; set; }
            public List<Route> Routes { get; set; } = new List<Route>();
        }

        public PokemonPage(DexEntry pkm) {
            InitializeComponent();
            loadingIcon.IsRunning = true;
            Title = pkm.Name;
            dexEntry = pkm;
        }

        private async Task PullPokemon() {
            if (await HasInternet()) {
                pokemon = await PokeData.GetPokemon(dexEntry.DexNum);
                pkmCaught.IsToggled = dexEntry.Obtained;
                pkmSprite.Source = dexEntry.Sprite;
                string type1 = pokemon.Types[0].Type.Name;
                BackgroundColor = Color.FromHex(PokeData.GetTypeColor(type1));

                if (pokemon.EncounterData.Count != 0) {
                    foreach (PokemonLocation location in pokemon.EncounterData) {
                        foreach (VersionDetail details in location.VersionDetails) {
                            var route = new Route {
                                Name = location.LocationArea.FormattedName,
                                Details = details
                            };
                            routes.Add(route);
                        }
                    }
                } else {
                    routes.Add(new Route { Name = "Cannot be caught" });
                }

                LocationList.ItemsSource = routes;
            }
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e) {
            dexEntry.Obtained = e.Value;
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            loadingIcon.IsRunning = true;
            await PullPokemon();
            loadingIcon.IsRunning = false;
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet", "Please connect to the internet", "Close");
                return false;
            }
            return true;
        }
    }
}
