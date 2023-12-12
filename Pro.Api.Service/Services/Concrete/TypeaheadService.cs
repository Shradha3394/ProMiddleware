using Bdx.Web.Api.Objects.Constants;
using Pro.Api.Service.Services.Abstract;
using System.Web;
using Pro.Web.Api.Library.Constants;

namespace Pro.Api.Service.Services.Concrete
{
    /// <summary>
    /// Typeahead Service will be used for calling TypeAhead API
    /// </summary>
    public class TypeaheadService : ITypeaheadService
    {
        private readonly IApiService _apiService;
        public TypeaheadService(IApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        /// Return TypeAhead API results corresponding to search text and partnerId
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public List<TypeAheadLocation> GetTypeAheadOptions(string searchText, int partnerId)
        {
            var parameters = new Dictionary<string, object>
            {
                { ApiUrlConstV2.PartnerId, partnerId },
                { ApiTypeAheadParams.SearchText, HttpUtility.UrlEncode(searchText) },
                { ApiTypeAheadParams.PageSize, ApiTypeAheadParams.PageSizeValue },
                { ApiTypeAheadParams.LocationTypes, ApiTypeAheadParams.DefaultLocationTypesValue },
                { ApiTypeAheadParams.SortBy, ApiTypeAheadParams.CommunityCount },
                { ApiTypeAheadParams.StartsWith, "true" }
            };
            return _apiService.GetDataWebApi<List<TypeAheadLocation>>(parameters, WebApiMethods.TypeaheadElasticLocations, partnerId);
        }
    }
}
