using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveDex.Models;

namespace LiveDex.Models {
    public static class PokeData {

        public const int MIN_DEX_NUM = 1;
        public const int MAX_DEX_NUM = 807;

        private const string END_POINT = "https://pokeapi.co/api/v2/pokemon/";

        private static Pokedex pokedex;

        private static Dictionary<int, bool> pkmCaught = null;

        public static Dictionary<int, bool> CaughtPokemonList() {
            if (pkmCaught == null) {
                pkmCaught = new Dictionary<int, bool>();
                for (int id = MIN_DEX_NUM; id <= MAX_DEX_NUM; id++) {
                    pkmCaught.Add(id, false);
                }
            }
            return pkmCaught;
        }

        public static string GetTypeColor(string type) {
            switch (type) {
            case "normal":
                return "#A8A878";
            case "fighting":
                return "#C03028";
            case "flying":
                return "#A891F0";
            case "posion":
                return "#A040A0";
            case "ground":
                return "#E0C068";
            case "rock":
                return "#B8A038";
            case "bug":
                return "#A8B820";
            case "ghost":
                return "#705898";
            case "steel":
                return "#B8B8D0";
            case "fire":
                return "#FA7C00";
            case "water":
                return "#6890F0";
            case "grass":
                return "#32CD32";
            case "electric":
                return "#FFC631";
            case "psychic":
                return "#F85888";
            case "ice":
                return "#98D8D8";
            case "dragon":
                return "#7038F8";
            case "dark":
                return "#705848";
            case "fariy":
                return "#EE99AC";
            default:
                return "#68A090";
            }
        }

        public static async Task<Pokemon> GetPokemon(int id) {

            string jsonContent = await GetJsonContent(END_POINT + id);
            if (jsonContent != null) {
                var pokedexEntry = Pokemon.FromJson(jsonContent);

                if (pokedexEntry != null) {
                    pokedexEntry.EncounterData = await GetPokemonLocation(pokedexEntry.LocationAreaEncounters.ToString());
                    //pokedexEntry.SpeciesData = await GetPokemonSpecies(pokedexEntry.Species.Url.ToString());
                    pokedexEntry.caught = CaughtPokemonList()[pokedexEntry.Id];
                    return pokedexEntry;
                }
            }
            return null;
        }

        private static async Task<List<PokemonLocation>> GetPokemonLocation(string pokemon) {
            string jsonContent = await GetJsonContent(pokemon);
            if (jsonContent != null) {
                var locationData = PokemonLocation.FromJson(jsonContent);
                if (locationData != null) {
                    return locationData;
                }
            }
            return null;
        }

        private static async Task<PokemonSpecies> GetPokemonSpecies(string pokemon) {
            string jsonContent = await GetJsonContent(pokemon);
            if (jsonContent != null) {
                var speciesData = PokemonSpecies.FromJson(jsonContent);
                if (speciesData != null) {
                    return speciesData;
                }
            }
            return null;
        }

        public static async Task<Pokedex> GetPokedexList() {
            string jsonContent = await GetJsonContent(END_POINT + "?limit=" + MAX_DEX_NUM);
            if (jsonContent != null) {
                pokedex = Pokedex.FromJson(jsonContent);
                if (pokedex != null) {
                    return pokedex;
                }
            }
            return null;
        }

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
