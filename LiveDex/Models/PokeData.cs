using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveDex.Models;
using Newtonsoft.Json;
using SQLite;

namespace LiveDex.Models {
    public static class PokeData {

        public const int MIN_DEX_NUM = 1;
        public const int MAX_DEX_NUM = 807;

        private const string POKEAPI_END_POINT = "https://pokeapi.co/api/v2/pokemon/";
        private const string POKENAMES_END_POINT = "https://raw.githubusercontent.com/sindresorhus/pokemon/master/data/en.json";

        private static Pokedex pokedex;

        public static string[] Names;

        public static CaughtModel[] Caught;

        public static async Task<Pokemon> GetPokemon(int id) {

            string jsonContent = await GetJsonContent(POKEAPI_END_POINT + id);
            if (jsonContent != null) {
                var pokedexEntry = Pokemon.FromJson(jsonContent);

                if (pokedexEntry != null) {
                    pokedexEntry.EncounterDetails = await GetPokemonLocation(pokedexEntry.LocationAreaEncounters.ToString());
                    //pokedexEntry.SpeciesData = await GetPokemonSpecies(pokedexEntry.Species.Url.ToString());
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
            string jsonContent = await GetJsonContent(POKEAPI_END_POINT + "?limit=" + MAX_DEX_NUM);
            if (jsonContent != null) {
                pokedex = Pokedex.FromJson(jsonContent);
                Names = await GetPokemonNames();
                Caught = (await App.CaughtDatabaseInstance.GetAllPokemonCaught()).ToArray();
                if (pokedex != null) {
                    return pokedex;
                }
            }
            return null;
        }

        private static async Task<string[]> GetPokemonNames() {
            string jsonContent = await GetJsonContent(POKENAMES_END_POINT);
            if (jsonContent != null) {
                var names = JsonConvert.DeserializeObject<List<string>>(jsonContent);
                if (pokedex != null) {
                    return names.ToArray();
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

        public static GenerationModel Gen1 = new GenerationModel {
            Name = "Gen 1",
            Region = "Kanto",
            DexStart = 0,
            DexEnd = 151
        };
        public static GenerationModel Gen2 = new GenerationModel {
            Name = "Gen 2",
            Region = "Johto",
            DexStart = 152,
            DexEnd = 251
        };
        public static GenerationModel Gen3 = new GenerationModel {
            Name = "Gen 3",
            Region = "Hoenn",
            DexStart = 252,
            DexEnd = 386
        };
        public static GenerationModel Gen4 = new GenerationModel {
            Name = "Gen 4",
            Region = "Sinnoh",
            DexStart = 387,
            DexEnd = 493
        };
        public static GenerationModel Gen5 = new GenerationModel {
            Name = "Gen 5",
            Region = "Sinnoh",
            DexStart = 494,
            DexEnd = 649
        };
        public static GenerationModel Gen6 = new GenerationModel {
            Name = "Gen 6",
            Region = "Unova",
            DexStart = 650,
            DexEnd = 721
        };
        public static GenerationModel Gen7 = new GenerationModel {
            Name = "Gen 7",
            Region = "Alola",
            DexStart = 722,
            DexEnd = 807
        };

        public static List<GenerationModel> Generations = new List<GenerationModel> {
            Gen1,Gen2,Gen3,Gen4,Gen5,Gen6,Gen7
        };

        public class GenerationModel {
            public string Name { get; set; }
            public string Region { get; set; }
            public int DexStart { get; set; }
            public int DexEnd { get; set; }
        }

    }

    public class CaughtModel {

        [PrimaryKey]
        public int ID { get; set; }
        public bool Obtained { get; set; }

    }



}
