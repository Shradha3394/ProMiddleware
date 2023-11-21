using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Pro.Web.Api.Library.Constants.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WebApiSearchType
    {
        Exact,
        Radius,
        Map,
        Polygon
    }
}
