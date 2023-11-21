using Bdx.Web.Api.Objects.Constants;
using Pro.Web.Api.Library.Constants.Enums;

namespace Pro.Web.Api.Library.Business
{
    [Serializable]
    public class SearchParams
    {
        public SearchParams()
        {
            Counties = new List<string>();
            PostalCodes = new List<string>();
            Cities = new List<string>();
            WebApiSearchType = WebApiSearchType.Exact;
            SrpType = SearchResultsPageType.Nothing;
            PhotoViewSortBy = SortBy.Random;
        }

        public void Init(bool fill = false)
        {
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            County = string.Empty;
            MarketName = string.Empty;
            StateName = string.Empty;
            //BuilderName = string.Empty;
            SchoolDistrictIds = string.Empty;
            CommName = string.Empty;
            try
            {
                SrpType =  SearchResultsPageType.HomeResults;
            }
            catch (Exception)
            {

                SrpType = SearchResultsPageType.HomeResults;
            }

            PageSize = 20;
            PageNumber = 1;
            GetLocationCoordinates = true;
            GetRadius = true;
            CustomRadiusSelected = false;
            PartnerId = 88;
            SortFirstBy = string.Empty;
            SortBy = SortBy.Random;
            PriceLow = 0;
            PriceHigh = 0;
            Bathrooms = 0;
            Bedrooms = 0;
            Garages = 0;
            LivingAreas = 0;
            Stories = 0;
            Green = false;
            Pool = false;
            GolfCourse = false;
            Gated = false;
            Parks = false;
            NatureAreas = false;
            Views = false;
            Waterfront = false;
            Sports = false;
            Adult = false;
            CommunityStatus = string.Empty;
            Zoom = 10;
           
        }
        public bool IsGridView { get; set; } = true;
        public bool IsPhotoView { get; set; }
        public bool IsMapView { get; set; }
        public IList<int> Builders { get; set; }
        public IList<int> Comms { get; set; }
        public IList<int> Markets { get; set; }
        public bool Adult { get; set; }
        public bool Sports { get; set; }
        public bool Waterfront { get; set; }
        public bool Views { get; set; }
        public bool Parks { get; set; }
        public bool Gated { get; set; }
        public bool Pool { get; set; }
        public bool Green { get; set; }
        public string SortFirstBy { get; set; }
        public SortBy SortBy { get; set; }
        public int PartnerId { get; set; }
        public int PageSize { get; set; }
        public string CommName { get; set; }
        public string StateName { get; set; }
        public string MarketName { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public List<string> Cities { get; set; }
        public List<string> PostalCodes { get; set; }
        public IList<string> Counties { get; set; }
        public int PageNumber { get; set; }
        public bool GolfCourse { get; set; }
        public bool NatureAreas { get; set; }
        public bool SingleFamily { get; set; }
        public bool MultiFamily { get; set; }
        public bool ConsumerPromo { get; set; }
        public bool AgentPromo { get; set; }
        public string CommunityStatus { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal Garages { get; set; }
        public int LivingAreas { get; set; }
        public decimal Stories { get; set; }
        public int MasterBedLocation { get; set; }
        public bool HasRVGarage { get; set; }
        public int PriceLow { get; set; }
        public int PriceHigh { get; set; }
        public int SqFtLow { get; set; }
        public int SqFtHigh { get; set; }
        public string SchoolDistrictIds { get; set; }
        public bool SortFacets { get; set; }
        public WebApiSearchType WebApiSearchType { get; set; }
        public bool ComingSoon { get; set; }
        public bool GetLocationCoordinates { get; set; }
        public bool GetRadius { get; set; }
        public bool CustomRadiusSelected { get; set; }
        public int Zoom { get; set; }
        //public string SchoolDistrict { get; set; }
        public string SchoolDistrictName { get; set; }
        public bool IsMultiLocationSearch { get; set; }
        public double CenterSearchWithMapLat { get; set; }
        public double CenterSearchWithMapLng { get; set; }
        public bool IsSavedSearch { get; set; }
        public int? ClientId { get; set; }
        public SearchResultsPageType SrpType { get; set; }
        public string SavedSearchName { get; set; }
        public bool GrandOpeningStatus { get; set; }
        public bool ComingSoonStatus { get; set; }
        public bool CloseOutStatus { get; set; }
        public bool NormalStatus { get; set; }
        public List<string> CommStatusList { get; set; } = new List<string>();
        public double MinLat { get; set; }
        public double MinLng { get; set; }
        public double MaxLat { get; set; }
        public double MaxLng { get; set; }

        public bool IsSearchWithMap => (MinLng != 0 || MinLat != 0 || MaxLat != 0 ||
                                        MaxLng != 0) && WebApiSearchType == WebApiSearchType.Map;

        public string SortByColumn { get; set; } = "";
        public int PhotoViewPageSize { get; set; } = 20;
        public int PhotoViewPageNumber { get; set; } = 1;
        public SortBy PhotoViewSortBy { get; set; } = SortBy.Random;
        public bool PhotoViewSortOrder { get; set; }
        public bool IsDrawInMap { get; set; }
        public bool ApiGeographyNew { get; set; }
        public List<ApiGeography> ApiGeography { get; set; } = new List<ApiGeography>();
        public List<string> ResponseParamList { get; set; } = new List<string>();
        public int BuilderId { get; set; }
        public bool IsUnpublished { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int FixedPageSize { get; set; } = 8;
        public int MarketId { get; set; }
        public double OriginLat { get; set; }
        public double OriginLng { get; set; }
        public int Radius { get; set; }
        public bool Event { get; set; }
        public bool SortOrder { get; set; }
        public bool ExcludeBasicListings { get; set; }
        public string PlanName { get; set; }
        public bool HotDeals { get; set; }
        public decimal NumStory { get; set; }
        public int CommId { get; set; }
        public int ParentCommId { get; set; }
        public int PlanId { get; set; }
        public int SpecId { get; set; }
        public int BrandId { get; set; }
        public bool Qmi { get; set; }
        public bool NoBoyl { get; set; }
        public bool Cache { get; set; }
        public bool LastCached { get; set; }
        public bool ExcludeBasiCommunities { get; set; }
        public string HomeStatus { get; set; }
        public bool CountsOnly { get; set; }
        public bool ExtCommDetail { get; set; }
        public bool ExtMapPoints { get; set; }
        public bool ExtHomeDetail { get; set; }
        public bool ExcludeImages { get; set; }
        public bool ExcludeVideos { get; set; }
        public bool ExcludeCountsAndFacets { get; set; }
        public bool ExcludeSummary { get; set; }
        public bool ExcludeCustomAmenities { get; set; }
        public bool ExcludeDescription { get; set; }
        public bool ExcludeInteractiveMedia { get; set; }
        public bool ExcludeFloorPlans { get; set; }
        public bool ExlucedeFloorPlanViewerUrl { get; set; }
        public bool ExcludeEnvisionUrl { get; set; }
        public bool ExcludeTollFreeNumber { get; set; }
        public bool ExcludeSchoolDistricts { get; set; }
        public bool ExcludeCommunityMap { get; set; }
        public bool ExcludeHomeOptions { get; set; }
        public bool ExcludeBuilderMap { get; set; }
        public bool ExcludeVideoTour { get; set; }
        public bool ExcludeAmenities { get; set; }
        public bool ExcludePromotions { get; set; }
        public bool ExcludeEvents { get; set; }
        public bool ExcludeAgents { get; set; }
        public bool ExcludeNonPdfBrochure { get; set; }
        public bool ExcludeUtilities { get; set; }
        public bool ExcludeFeesAndTaxes { get; set; }
        public bool? BasicListingToTheEnd { get; set; }
        public int MaxReturnRows { get; set; }
        public bool AgeRestricted { get; set; }
        public bool Promo { get; set; }
        public string ApiGeographyPolygonIntersects { get; set; }
        public IList<int> CommIdsSort { get; set; }
        public IList<int> PlanIdsSort { get; set; }
        public IList<int> SpecIdsSort { get; set; }
        public IList<int> Brands { get; set; }

        #region ApiSearchParams
        public string SpecIds { get; set; }
        public int TotalRecords { get; set; }
        public bool IsReset { get; set; }
        public LocationParam LocationType { get; set; } = new LocationParam();
        public bool CustomBuilderLocations { get; set; }
        public bool ExcludeFacetsOnly { get; set; }
        public bool IncludeBrandShowcase { get; set; }
        public bool ShowMarketsFacet { get; set; }
        public bool IncludeAgentCompensation { get; set; }
        public bool IncludePromos { get; set; }
        public bool IncludeEvents { get; set; }

        #endregion
    }
}
