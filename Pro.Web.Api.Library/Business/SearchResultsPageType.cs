using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Pro.Web.Api.Library.Business
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchResultsPageType
    {
        Nothing = 0,
        CommunityResults = 1,
        HomeResults = 2
    }
}

