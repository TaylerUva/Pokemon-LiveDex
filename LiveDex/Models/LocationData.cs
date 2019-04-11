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

    public partial class LocationData {
        [JsonProperty("location_area")]
        public LocationArea LocationArea { get; set; }

        [JsonProperty("version_details")]
        public List<VersionDetail> VersionDetails { get; set; }
    }

    public partial class LocationArea {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class VersionDetail {
        [JsonProperty("encounter_details")]
        public List<EncounterDetail> EncounterDetails { get; set; }

        [JsonProperty("max_chance")]
        public long MaxChance { get; set; }
    }

    public partial class EncounterDetail {
        [JsonProperty("chance")]
        public long Chance { get; set; }

        [JsonProperty("condition_values")]
        public List<LocationArea> ConditionValues { get; set; }

        [JsonProperty("max_level")]
        public long MaxLevel { get; set; }

        [JsonProperty("method")]
        public LocationArea Method { get; set; }

        [JsonProperty("min_level")]
        public long MinLevel { get; set; }
    }

    public partial class LocationData {
        public static List<LocationData> FromJson(string json) => JsonConvert.DeserializeObject<List<LocationData>>(json, Converter.Settings);
    }
}
