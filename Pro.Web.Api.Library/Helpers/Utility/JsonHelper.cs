using Newtonsoft.Json;
using Pro.Web.Api.Library.NewtonsoftJson;

namespace Pro.Web.Api.Library.Helpers.Utility
{
    public static class JsonHelper
    {
        public static T ToFromJson<T>(this string jSonString) where T : new()
        {
            return FromJson<T>(jSonString);
        }

        public static T FromJson<T>(string jSonString)
        {
            return JsonConvert.DeserializeObject<T>(jSonString);
        }

        public static string ToJson(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static string ToJson(this object obj, JsonSerializerSettings settings)
        {
            var json = JsonConvert.SerializeObject(obj, settings);
            return json;
        }

        public static string ToJson(this object obj, Formatting formatting)
        {
            var json = JsonConvert.SerializeObject(obj, formatting);
            return json;
        }

        public static JsonSerializerSettings CleanJson = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = ShouldSerializeContractResolver.Instance
        };
    }
}