﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using LiveDex.Models;
//
//    var pokemonSpecies = PokemonSpecies.FromJson(jsonString);

namespace LiveDex.Models {
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PokemonSpecies {
        [JsonProperty("base_happiness")]
        public long BaseHappiness { get; set; }

        [JsonProperty("capture_rate")]
        public long CaptureRate { get; set; }

        [JsonProperty("color")]
        public NameURL Color { get; set; }

        [JsonProperty("egg_groups")]
        public List<NameURL> EggGroups { get; set; }

        [JsonProperty("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }

        [JsonProperty("evolves_from_species")]
        public object EvolvesFromSpecies { get; set; }

        [JsonProperty("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonProperty("form_descriptions")]
        public List<object> FormDescriptions { get; set; }

        [JsonProperty("forms_switchable")]
        public bool FormsSwitchable { get; set; }

        [JsonProperty("gender_rate")]
        public long GenderRate { get; set; }

        [JsonProperty("genera")]
        public List<Genus> Genera { get; set; }

        [JsonProperty("generation")]
        public NameURL Generation { get; set; }

        [JsonProperty("growth_rate")]
        public NameURL GrowthRate { get; set; }

        [JsonProperty("habitat")]
        public NameURL Habitat { get; set; }

        [JsonProperty("has_gender_differences")]
        public bool HasGenderDifferences { get; set; }

        [JsonProperty("hatch_counter")]
        public long HatchCounter { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("names")]
        public List<Name> Names { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("pal_park_encounters")]
        public List<PalParkEncounter> PalParkEncounters { get; set; }

        [JsonProperty("pokedex_numbers")]
        public List<PokedexNumber> PokedexNumbers { get; set; }

        [JsonProperty("shape")]
        public NameURL Shape { get; set; }

        [JsonProperty("varieties")]
        public List<Variety> Varieties { get; set; }
    }

    public partial class EvolutionChain {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class FlavorTextEntry {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public NameURL Language { get; set; }

        [JsonProperty("version")]
        public NameURL Version { get; set; }
    }

    public partial class Genus {
        [JsonProperty("genus")]
        public string GenusGenus { get; set; }

        [JsonProperty("language")]
        public NameURL Language { get; set; }
    }

    public partial class Name {
        [JsonProperty("language")]
        public NameURL Language { get; set; }

        [JsonProperty("name")]
        public string NameName { get; set; }
    }

    public partial class PalParkEncounter {
        [JsonProperty("area")]
        public NameURL Area { get; set; }

        [JsonProperty("base_score")]
        public long BaseScore { get; set; }

        [JsonProperty("rate")]
        public long Rate { get; set; }
    }

    public partial class PokedexNumber {
        [JsonProperty("entry_number")]
        public long EntryNumber { get; set; }

        [JsonProperty("pokedex")]
        public NameURL Pokedex { get; set; }
    }

    public partial class Variety {
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty("pokemon")]
        public NameURL Pokemon { get; set; }
    }

    public partial class PokemonSpecies {
        public static PokemonSpecies FromJson(string json) => JsonConvert.DeserializeObject<PokemonSpecies>(json, Converter.Settings);
    }
}
