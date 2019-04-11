﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using LiveDex.Models;
//
//    var pokedex = Pokedex.FromJson(jsonString);

namespace LiveDex.Models {
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Pokedex {

        [JsonProperty("results")]
        public List<Entry> Entries { get; set; }
    }

    public partial class Entry {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        public int id {
            //get { return Url.Segments[6]; }
            set { }
        }

        public string imgURL {
            get { return "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + Url.Segments[6]; }
            set { }
        }
    }

    public partial class Pokedex {
        public static Pokedex FromJson(string json) => JsonConvert.DeserializeObject<Pokedex>(json, Converter.Settings);
    }

    public static class Serialize {
        public static string ToJson(this Pokedex self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
