using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Pro.Web.Api.Library.Constants.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortBy
    {
        /// <summary>Random</summary>
        Random,
        /// <summary>City</summary>
        City,
        /// <summary>County</summary>
        County,
        /// <summary>PostalCode</summary>
        PostalCode,
        /// <summary>NumHomes</summary>
        NumHomes,
        /// <summary>HasPhotos</summary>
        HasPhotos,
        /// <summary>NumPhotos</summary>
        NumPhotos,
        /// <summary>Distance</summary>
        Distance,
        /// <summary>Plan</summary>
        Plan,
        /// <summary>AddressPlan</summary>
        AddressPlan,
        /// <summary>Bed</summary>
        Bed,
        /// <summary>Price</summary>
        Price,
        /// <summary>PriceLow</summary>
        PriceLow,
        /// <summary>Brand</summary>
        Brand,
        /// <summary>Comm</summary>
        Comm,
        /// <summary>HomeStatus</summary>
        HomeStatus,
        /// <summary>DefaultMpc</summary>
        DefaultMpc,
        /// <summary>PriceRange</summary>
        PriceRange,
        /// <summary>BedRange</summary>
        BedRange,
        /// <summary>BathRange</summary>
        BathRange,
        /// <summary>Sqft</summary>
        Sqft,
        /// <summary>SqftRange</summary>
        SqftRange,
        /// <summary>AppendToList</summary>
        AppendToList,
        /// <summary>MarketId</summary>
        MarketId,
        /// <summary>AvailabilityAndStatus</summary>
        AvailabilityAndStatus,
        /// <summary>PreferredScore</summary>
        PreferredScore,
        /// <summary>Sorts by Comm Id or Listing Id (Creation Date)</summary>
        Id,
        /// <summary>HighPriceNhsMobile</summary>
        HighPriceNhsMobile,
        /// <summary>LowPriceNhsMobile</summary>
        LowPriceNhsMobile,
        /// <summary>DistanceForMobile</summary>
        DistanceForMobile,
        /// <summary>Sort by Image Id, used with the sort first by (sending image id there) </summary>
        Image,
        /// <summary>
        /// Sort By Community Similarity.  Requires the parameter Sort By Similar To: Community Id
        /// </summary>
        SimilarCommunities,
        /// <summary>
        /// Sort By Home Similarity. Requires the parameter Sort By Similar To: Listing Id
        /// </summary>
        SimilarHome,
        /// <summary>Sort By Builder Id</summary>
        BuilderId,
        /// <summary>Sort By Agent Co-Op Code</summary>
        CoOp,
        TotalReviews,
        /// <summary>
        /// Sort by postal code and special order for basics listings
        /// </summary>
        PostalCodeWithBasics,
    }
}
