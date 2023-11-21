namespace Pro.Api.Service.Services.Abstract
{
    public interface ITypeaheadService
    {
        List<TypeAheadLocation> GetTypeAheadOptions(string searchText, int partnerId);
    }
}
