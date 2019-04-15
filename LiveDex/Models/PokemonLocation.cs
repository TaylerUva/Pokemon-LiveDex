﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using LiveDex.Models;
//
//    var locationData = LocationData.FromJson(jsonString);

namespace LiveDex.Models {
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PokemonLocation {
        [JsonProperty("location_area")]
        public NameURL LocationArea { get; set; }

        [JsonProperty("version_details")]
        public List<LocationVersionDetail> VersionDetails { get; set; }
    }

    public partial class LocationVersionDetail {
        [JsonProperty("encounter_details")]
        public List<EncounterDetail> EncounterDetails { get; set; }

        [JsonProperty("max_chance")]
        public long MaxChance { get; set; }

        [JsonProperty("version")]
        public NameURL Version { get; set; }
    }

    public partial class EncounterDetail {
        [JsonProperty("chance")]
        public long Chance { get; set; }

        [JsonProperty("condition_values")]
        private List<NameURL> ConditionsList { get; set; }

        public string ConditionsString {
            get {
                var conditionsStringList = new List<string>();
                foreach (NameURL condition in ConditionsList) {
                    conditionsStringList.Add(condition.FormattedName);
                }
                return string.Join(", ", conditionsStringList);
            }
            set { }
        }

        [JsonProperty("max_level")]
        public long MaxLevel { get; set; }

        [JsonProperty("method")]
        public NameURL Method { get; set; }

        [JsonProperty("min_level")]
        public long MinLevel { get; set; }
    }

    public class Route {
        public string Name { get; set; }
        public LocationVersionDetail Details { get; set; }
    }

    public class Game {
        public string Name { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();
    }

    public partial class PokemonLocation {
        public static List<PokemonLocation> FromJson(string json) => JsonConvert.DeserializeObject<List<PokemonLocation>>(json, Converter.Settings);
    }
}
