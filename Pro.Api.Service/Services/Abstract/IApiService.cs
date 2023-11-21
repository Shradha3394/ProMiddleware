namespace Pro.Api.Service.Services.Abstract
{
    public interface IApiService
    {
        Task<T> GetDataWebApiAsync<T>(Dictionary<string, object> searchParams, string searchType, int partnerId) where T : new();
        Task<T> GetDataWebApiAsync<T>(SearchParams searchParams, string searchType) where T : new();
        Task<T> PostDataWebApiAsync<T>(int partnerId, string url, string json) where T : new();
        T GetDataWebApi<T>(SearchParams searchParams, string searchType) where T : new();
        T GetDataWebApi<T>(Dictionary<string, object> searchParams, string searchType, int partnerId) where T : new();
        T PostDataWebApi<T>(int partnerId, string url, string json) where T : new();
    }
}
