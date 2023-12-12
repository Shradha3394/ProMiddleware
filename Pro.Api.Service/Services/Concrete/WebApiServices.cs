using System.Collections.Concurrent;
using System.Text;
using Bdx.Web.Api.Objects.Constants;
using Newtonsoft.Json;
using Nhs.Utility.Common;
using Pro.Web.Api.Library.Constants.Enums;

namespace Pro.Api.Service.Services.Concrete
{
    public class WebApiServices
    {
        private readonly ConcurrentDictionary<string, object> _parameters;
        private readonly HttpClient _client = new HttpClient();
        public WebApiServices()
        {
            _parameters = new ConcurrentDictionary<string, object>();
        }

        public WebApiServices(int partnerId) : this()
        {
            if (!_parameters.ContainsKey(ApiUrlConstV2.PartnerId))
                _parameters.GetOrAdd(ApiUrlConstV2.PartnerId, partnerId);
        }

        public async Task<T> DataAsStreamAsync<T>(string page, HttpMethod method, HttpContent stringContent = null, CancellationToken cancellationToken = new CancellationToken())
        {
            Int();
            var url = Url(page);
            using (var request = new HttpRequestMessage(method, url))
            {
                request.Content = stringContent;
                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return DeserializeJsonFromStream<T>(stream);
                    }
                    var content = await StreamToStringAsync(stream).ConfigureAwait(false);
                    ErrorLogger.LogError("API URL: " + url);
                    ErrorLogger.LogError("Content: " + content);
                }
            }
            return default(T);
        }



        public void SetParameters(Dictionary<string, object> parameters)
        {
            foreach (var p in parameters.Where(p => !_parameters.ContainsKey(p.Key)))
            {
                _parameters.GetOrAdd(p.Key, p.Value);
            }
        }

        public string Url(string page)
        {
            var searchUrl = new StringBuilder();
            searchUrl.Append("https://sprint-api.newhomesource.com/api/");
            searchUrl.AppendFormat("{0}/{1}?", WebApiVersions, page);
            foreach (var parameter in _parameters)
            {
                searchUrl.AppendFormat("{0}={1}&", parameter.Key, parameter.Value);
            }
            return searchUrl.ToString().TrimEnd('&');
        }

        private string GenerateSessionToken()
        {
            var token = WebApiSecurity.CreateToken(DateTime.UtcNow, "md5",
                PartnerSitePassword);
            return token;
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
            {
                return default(T);
            }

            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jsonTextReader);
                return searchResult;
            }
        }
        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content;

            if (stream == null)
            {
                return null;
            }

            using (var sr = new StreamReader(stream))
            {
                content = await sr.ReadToEndAsync();
            }

            return content;
        }

        private void Int()
        {
            WebApiVersions = WebApiVersions.V2;
            _parameters.GetOrAdd("client", "NhsPro");
            _parameters.GetOrAdd("sessiontoken", GenerateSessionToken());
            _parameters.GetOrAdd("algorithm", "md5");

        }

        public string PartnerSitePassword { get; set; }
        public WebApiVersions WebApiVersions { get; set; }
        public int PartnerId { get; set; }
    }
}