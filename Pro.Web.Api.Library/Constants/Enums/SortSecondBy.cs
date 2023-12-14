using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Pro.Web.Api.Library.Constants.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortSecondBy
    {
        /// <summary>The collection will be retrieved in no special order.</summary>
        None,
        /// <summary>
        /// The collection will be retrieved in random order.
        /// This sort is based on the value of the SortKey field.
        /// </summary>
        Random,
        /// <summary>
        /// The collection will be retrieved based on the distance of the homes or communities given by the location,
        /// basically, the latitude and longitude specified on the search parameters (Radio search).
        /// </summary>
        Distance,
        /// <summary>The collection will be sorted by the price of the homes (PriceLo/PrLo).</summary>
        Price,
        /// <summary>The collection will be sorted by brand name.</summary>
        Brand,
        /// <summary>The collection will be sorted by community name</summary>
        Comm,
        /// <summary>The collection will be sorted by the square feet of the homes (SqFtLow/SftLo).</summary>
        Sqft,
        /// <summary>
        /// The collection will be sorted by the number of homes that have a community.
        /// This applies only to community search-type or brand search-type.
        /// </summary>
        NumHomes,
        /// <summary>The collection will be sorted by city name.</summary>
        City,
        /// <summary>The collection will be sorted by the number of beds in a home (MinBeds/BrLo).</summary>
        Bed,
        /// <summary>The collection will be sorted by plan name.</summary>
        Plan,
    }
}
