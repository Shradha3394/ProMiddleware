
namespace Pro.Web.Api.Library.Business
{
    /// <summary>
    /// Input params required for calling Typeahead API
    /// </summary>
    public class ApiTypeAheadParams
    {
        public const string SearchText = "searchTerm";
        public const string PageSize = "resultNumber";
        public const string LocationTypes = "types";
        public const int PageSizeValue = 20;
        public const string DefaultLocationTypesValue = "Market,City,County,Zip,Community,Developer,School,SchoolSrp";
        public const string SortBy = "sortby";
        public const string CommunityCount = "communitycount";
        public const string StartsWith = "startswith";
    }
}