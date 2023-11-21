using Newtonsoft.Json;

namespace Pro.Web.Api.Library.Business
{
    [JsonObject]
    [Serializable]
    public class ApiGeography
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return Longitude + " " + Latitude;
        }
    }
}