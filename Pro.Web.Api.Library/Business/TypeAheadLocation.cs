using Pro.Api.Model.Concrete;
using Newtonsoft.Json;
using Pro.Api.Model.Constants;
using Pro.Api.Model;

namespace Pro.Web.Api.Library.Business
{
    public class TypeAheadLocation : ApiTypeaheadLocation
    {
        public int LocationTypePriority => !string.IsNullOrEmpty(Type)
            ? (int) Enum.Parse(typeof(LocationType), Type)
            : (int) LocationType.Market;

        [JsonIgnore] public ProMarket Market { get; set; }
    }
}