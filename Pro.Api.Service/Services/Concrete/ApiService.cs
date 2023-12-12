using Pro.Api.Service.Services.Abstract;
using System.Text;
using Pro.Api.Model.Concrete;
using Pro.Web.Api.Library.Helpers.WebApiServices;

namespace Pro.Api.Service.Services.Concrete
{
    public class ApiService : IApiService
    {
        private readonly IPartnerService _partnerService;
        private ProPartnerPassword _partner;

        public ApiService(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        //public void SetPartner(int partnerId)
        //{
        //    _partner = _partnerService.GetPartnerPassword(partnerId);
        //}


        /// <summary>
        /// Used to get data asynchronously from api/V2/
        /// </summary>
        /// <param name="searchParams">searchParams Dictionary object</param>
        /// <param name="searchType">searchType</param>
        /// <param name="partnerId">partnerId</param>
        /// <returns>API response</returns>
        public async Task<T> GetDataWebApiAsync<T>(Dictionary<string, object> searchParams, string searchType, int partnerId) where T : new()
        {
            return await DataAsStreamAsyncGet<T>(partnerId, searchParams, searchType).ConfigureAwait(false);
        }

        /// <summary>
        /// Used to get data asynchronously from api/V2/
        /// </summary>
        /// <param name="searchParams">searchParams object</param>
        /// <param name="searchType">searchType</param>
        /// <returns>API response</returns>
        public async Task<T> GetDataWebApiAsync<T>(SearchParams searchParams, string searchType) where T : new()
        {
            var @params = searchParams.ToWebApiParameters();
            return await DataAsStreamAsyncGet<T>(searchParams.PartnerId, @params, searchType).ConfigureAwait(false);
        }

        /// <summary>
        /// Used to post data asynchronously to api/V2/
        /// </summary>
        /// <param name="partnerId">partnerId</param>
        /// <param name="url">page url</param>
        /// <param name="json">post json data</param>
        /// <returns>API response</returns>
        public async Task<T> PostDataWebApiAsync<T>(int partnerId, string url, string json) where T : new()
        {
            return await DataAsStreamAsyncPost<T>(partnerId, url, json).ConfigureAwait(false);
        }

        public T GetDataWebApi<T>(SearchParams searchParams, string searchType) where T : new()
        {
            var @params = searchParams.ToWebApiParameters();
            return DataAsStreamAsyncGet<T>(searchParams.PartnerId, @params, searchType).GetAwaiter().GetResult();
        }

        public T GetDataWebApi<T>(Dictionary<string, object> searchParams, string searchType, int partnerId) where T : new()
        {
            return DataAsStreamAsyncGet<T>(partnerId, searchParams, searchType).GetAwaiter().GetResult();
        }

        public T PostDataWebApi<T>(int partnerId, string url, string json) where T : new()
        {
            return DataAsStreamAsyncPost<T>(partnerId, url, json).GetAwaiter().GetResult();
        }

        private async Task<T> DataAsStreamAsyncPost<T>(int partnerId, string url, string json) where T : new()
        {
            var webApiServices = GetWebApiService(partnerId);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await webApiServices.DataAsStreamAsync<T>(url, HttpMethod.Post, stringContent).ConfigureAwait(false);
            return data;
        }

        private async Task<T> DataAsStreamAsyncGet<T>(int partnerId, Dictionary<string, object> searchParams, string searchType) where T : new()
        {
            var webApiServices = GetWebApiService(partnerId);
            webApiServices.SetParameters(searchParams);
            var data = await webApiServices.DataAsStreamAsync<T>(searchType, HttpMethod.Get).ConfigureAwait(false);
            return data;
        }

        private WebApiServices GetWebApiService(int partnerId)
        {
            //if (_partner == null)
            //    _partner = _partnerService.GetPartnerPassword(partnerId);

            //if (_partner == null)
            //    return default;

            var partnerSitePassword = "7011c3dc";
            return new WebApiServices(partnerId)
            {
                PartnerSitePassword = partnerSitePassword
            };
        }
    }
}
