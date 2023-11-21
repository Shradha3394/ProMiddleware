using Newtonsoft.Json;

namespace Pro.Api.Model.Concrete
{
    public class Location
    {
        public string Name { get; set; }

        // Type of the location as described below
        // 1 : Market
        // 2 : City
        // 3 : County
        // 4 : Zip
        // 5 : Comm name
        // 6 : Developer Name 
        // 8 : School 
        public int Type { get; set; }
        public int Id { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool StartWith { get; set; }
        [JsonIgnore]
        public ProMarket Market { get; set; }
        public string BrandName { get; set; }
    }
}
