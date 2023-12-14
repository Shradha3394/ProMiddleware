using Newtonsoft.Json;

namespace Pro.Api.Model
{
    /// <summary>ApiTypeaheadLocation</summary>
    public class ApiTypeaheadLocation
    {
        /// <summary>Id</summary>
        public int Id { get; set; }

        /// <summary>Name</summary>
        public string Name { get; set; }

        /// <summary>Type</summary>
        public string Type { get; set; }

        /// <summary>Market Id</summary>
        public int MarketId { get; set; }

        /// <summary>Market Name</summary>
        public string MarketName { get; set; }

        /// <summary>Market State Abbr</summary>
        public string MarketStateAbbr { get; set; }

        /// <summary>Market State Name</summary>
        public string MarketStateName { get; set; }

        /// <summary>Latitude</summary>
        public Decimal? Latitude { get; set; }

        /// <summary>Longitude</summary>
        public Decimal? Longitude { get; set; }

        /// <summary>City</summary>
        public string City { get; set; }

        /// <summary>State</summary>
        public string State { get; set; }

        /// <summary>Used for types brands only, shows if the brand has builder show case </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasShowCase { get; set; }

        /// <summary>Returned only for brands, shows if the brand is CST or CSL </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BrandType { get; set; }

        /// <summary>Returned only for brands, shows if the brand is CST or CSL </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BrandName { get; set; }

        /// <summary>Returned only for brands, shows if the brand is CST or CSL </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? BrandId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CommunityCount { get; set; }
    }
}
