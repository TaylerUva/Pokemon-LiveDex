using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveDex.Models;

namespace LiveDex.Models {
    public static class GetPokedex {

        public const int MIN_DEX_NUM = 1;
        //public const int MAX_DEX_NUM = 807;
        public const int MAX_DEX_NUM = 100;

        private const string END_POINT = "https://pokeapi.co/api/v2/pokemon/";

        private static List<Pokemon> FullPokedex = new List<Pokemon>();

        public static async Task<Pokemon> GetEntryAsync(int id) {

            string jsonContent = await GetJsonContent(END_POINT + id);
            if (jsonContent != null) {
                var pokedexEntry = Pokemon.FromJson(jsonContent);

                if (pokedexEntry != null) {
                    pokedexEntry.EncounterData = await GetLocationDataAsync(pokedexEntry.LocationAreaEncounters);
                    return pokedexEntry;
                }
            }
            return null;
        }

        private static async Task<List<PokemonLocation>> GetLocationDataAsync(string pokemon) {
            string jsonContent = await GetJsonContent(pokemon);
            if (jsonContent != null) {
                var locationData = PokemonLocation.FromJson(jsonContent);
                if (locationData != null) {
                    return locationData;
                }
            }
            return null;
        }

        //public static async Task<List<Pokemon>> GetSurfaceData() {


        //}

        private static async Task<string> GetJsonContent(string URL) {
            Uri uri = new Uri(URL);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

    }
}
