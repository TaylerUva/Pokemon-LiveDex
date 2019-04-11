using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveDex.Models;

namespace LiveDex.Models {
    public static class GetPokedex {

        private const string END_POINT = "https://pokeapi.co/api/v2/pokemon/";

        private static Pokedex pokedexEntry;

        public static async Task<Pokedex> GetEntryAsync(string id) {
            // PULL DATA
            Uri pokeAPIUri = new Uri(END_POINT + id);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(pokeAPIUri);

            if (response.IsSuccessStatusCode) {
                // PULL DATA
                string jsonContent = await response.Content.ReadAsStringAsync();
                pokedexEntry = Pokedex.FromJson(jsonContent);

                if (pokedexEntry != null) {
                    pokedexEntry.EncounterData = await GetLocationDataAsync(pokedexEntry.LocationAreaEncounters);
                    return pokedexEntry;
                }
            }
            return null;
        }

        public static async Task<List<LocationData>> GetLocationDataAsync(Uri pokemon) {
            // PULL DATA
            Uri locationUri = pokemon;

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(locationUri);

            if (response.IsSuccessStatusCode) {
                // PULL DATA
                string jsonContent = await response.Content.ReadAsStringAsync();
                var locationData = LocationData.FromJson(jsonContent);

                if (locationData != null) {
                    return locationData;
                }
            }
            return null;
        }
    }
}
