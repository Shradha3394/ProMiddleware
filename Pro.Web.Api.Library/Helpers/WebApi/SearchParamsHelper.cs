using Bdx.Web.Api.Objects;
using Bdx.Web.Api.Objects.Constants;
using MongoDB.Bson;
using Nhs.Utility.Common;
using Pro.Api.Model.Concrete;
using Pro.Web.Api.Library.Business;
using Pro.Web.Api.Library.Business.Utils;
using Pro.Web.Api.Library.Constants.Enums;
using Pro.Web.Api.Library.Helpers.Utility;

namespace Pro.Web.Api.Library.Helpers.WebApiServices
{
    public static class SearchParamsHelper
    {
        // ReSharper disable once FunctionComplexityOverflow
        public static Dictionary<string, object> ToWebApiParameters(this SearchParams searchParams)
        {
            var @params = new Dictionary<string, object> { { ApiUrlConstV2.PartnerId, searchParams.PartnerId } };

            if (searchParams.WebApiSearchType == WebApiSearchType.Exact)
            {
                if (!string.IsNullOrEmpty(searchParams.City))
                    @params.Add(ApiUrlConstV2.City, searchParams.City);
                else if (!string.IsNullOrEmpty(searchParams.County))
                    @params.Add(ApiUrlConstV2.County, searchParams.County);
                else if (!string.IsNullOrEmpty(searchParams.PostalCode))
                    @params.Add(ApiUrlConstV2.PostalCode, searchParams.PostalCode);
                else if (searchParams.Cities != null && searchParams.Cities.Any(p => !string.IsNullOrEmpty(p)))
                    @params.Add(ApiUrlConstV2.Cities, searchParams.Cities.Where(p => !string.IsNullOrEmpty(p)).ToArray().Join(searchParams.Cities[0].Contains(",") ? ";" : ","));
                else if (searchParams.Counties != null && searchParams.Counties.Any(p => !string.IsNullOrEmpty(p)))
                    @params.Add(ApiUrlConstV2.Counties, searchParams.Counties.Where(p => !string.IsNullOrEmpty(p)).ToArray().Join(searchParams.Counties[0].Contains(",") ? ";" : ","));
                else if (searchParams.PostalCodes != null && searchParams.PostalCodes.Any(p => !string.IsNullOrEmpty(p)))
                    @params.Add(ApiUrlConstV2.Postalcodes, searchParams.PostalCodes.Where(p => !string.IsNullOrEmpty(p)).ToArray().Join(searchParams.PostalCodes[0].Contains(",") ? ";" : ","));
                else if (searchParams.Markets != null && searchParams.Markets.Any(p => p > 0))
                    @params.Add(ApiUrlConstV2.Markets, string.Join(",", searchParams.Markets.Where(p => p > 0)));

                if (searchParams.MarketId > 0)
                    @params.Add(ApiUrlConstV2.MarketId, searchParams.MarketId);

                var addStateParam = !string.IsNullOrEmpty(searchParams.City) || !string.IsNullOrEmpty(searchParams.County) || !string.IsNullOrEmpty(searchParams.PostalCode);
                if (addStateParam && !string.IsNullOrEmpty(searchParams.State))
                    @params.Add(ApiUrlConstV2.State, searchParams.State);
            }
            else if (searchParams.WebApiSearchType == WebApiSearchType.Radius)
            {
                if (searchParams.OriginLat != 0)
                    @params.Add(ApiUrlConstV2.OriginLat, searchParams.OriginLat);
                if (searchParams.OriginLng != 0)
                    @params.Add(ApiUrlConstV2.OriginLng, searchParams.OriginLng);
                if (searchParams.Radius > 0)
                    @params.Add(ApiUrlConstV2.Radius, searchParams.Radius);
            }
            //Location
            else if (searchParams.WebApiSearchType == WebApiSearchType.Map)
            {
                if (searchParams.MinLat != 0)
                    @params.Add(ApiUrlConstV2.MinLat, searchParams.MinLat);
                if (searchParams.MinLng != 0)
                    @params.Add(ApiUrlConstV2.MinLng, searchParams.MinLng);
                if (searchParams.MaxLat != 0)
                    @params.Add(ApiUrlConstV2.MaxLat, searchParams.MaxLat);
                if (searchParams.MaxLng != 0)
                    @params.Add(ApiUrlConstV2.MaxLng, searchParams.MaxLng);
            }
            else if (searchParams.WebApiSearchType == WebApiSearchType.Polygon)
            {
                // Polygon Search can not be a Get call, used the Post option
            }

            #region Promos
            if (searchParams.Promo)
            {
                @params.Add(ApiUrlConstV2.Promo, 1);
            }
            else
            {
                if (searchParams.ConsumerPromo)
                    @params.Add(ApiUrlConstV2.CPromo, 1);
                else if (searchParams.AgentPromo)
                    @params.Add(ApiUrlConstV2.APromo, 1);
            }

            #endregion

            if (searchParams.Event)
            {
                @params.Add(ApiUrlConstV2.Events, 1);
            }
            if (searchParams.PageSize > 0)
                @params.Add(ApiUrlConstV2.PageSize, searchParams.PageSize);

            if (searchParams.PageNumber > 0)
                @params.Add(ApiUrlConstV2.Page, searchParams.PageNumber);

            if (searchParams.SortBy == SortBy.Random)
            {
                if (!string.IsNullOrWhiteSpace(searchParams.City))
                    @params.Add(ApiUrlConstV2.SortBy, SortBy.City);
                else if (!string.IsNullOrWhiteSpace(searchParams.County))
                    @params.Add(ApiUrlConstV2.SortBy, SortBy.County);
                else if (!string.IsNullOrWhiteSpace(searchParams.PostalCode))
                    @params.Add(ApiUrlConstV2.SortBy, SortBy.PostalCode);
                else
                    @params.Add(ApiUrlConstV2.SortBy, SortBy.Random);


                if (!string.IsNullOrEmpty(searchParams.SortFirstBy) &&
                    !string.IsNullOrEmpty(searchParams.City) ||
                    !string.IsNullOrEmpty(searchParams.County) ||
                    !string.IsNullOrEmpty(searchParams.PostalCode))
                {
                    @params.Add(ApiUrlConstV2.SortFirstBy, searchParams.SortFirstBy);
                    @params.Add(ApiUrlConstV2.SortSecondBy, SortSecondBy.Random);
                }
            }
            else
                @params.Add(ApiUrlConstV2.SortBy, searchParams.SortBy);

            if (searchParams.SortOrder && searchParams.SortBy != SortBy.Random)
                @params.Add(ApiUrlConstV2.SortOrder, 1);

            if (searchParams.ExcludeBasicListings)
                @params.Add(ApiUrlConstV2.ExcludeBasicListings, 1);

            if (searchParams.BuilderId > 0)
                @params.Add(ApiUrlConstV2.BuilderId, searchParams.BuilderId);

            //Home Attribs
            if (searchParams.Bathrooms > 0)
                @params.Add(ApiUrlConstV2.Bath, searchParams.Bathrooms);

            if (searchParams.Bedrooms > 0)
                @params.Add(ApiUrlConstV2.Bed, searchParams.Bedrooms);

            if (searchParams.Garages > 0)
                @params.Add(ApiUrlConstV2.Gar, searchParams.Garages);

            if (searchParams.PlanName.IsNotNullOrWhiteSpaceOrEmpty())
                @params.Add(ApiUrlConstV2.PlanName, searchParams.PlanName);

            //******* BEGIN Amenities ************
            if (searchParams.Pool)
                @params.Add(ApiUrlConstV2.Pool, 1);
            if (searchParams.Green)
                @params.Add(ApiUrlConstV2.Green, 1);
            if (searchParams.NatureAreas)
                @params.Add(ApiUrlConstV2.Nature, 1);
            if (searchParams.GolfCourse)
                @params.Add(ApiUrlConstV2.Golf, 1);
            if (searchParams.Waterfront)
                @params.Add(ApiUrlConstV2.WaterFront, 1);
            if (searchParams.Parks)
                @params.Add(ApiUrlConstV2.Parks, 1);
            if (searchParams.Views)
                @params.Add(ApiUrlConstV2.Views, 1);
            if (searchParams.Sports)
                @params.Add(ApiUrlConstV2.Sports, 1);
            if (searchParams.Adult)
                @params.Add(ApiUrlConstV2.Adult, 1);
            if (searchParams.Gated)
                @params.Add(ApiUrlConstV2.Gated, 1);

            //******* END Amenities ************

            //******* BEGIN Price ************
            if (searchParams.PriceLow > 0)
                @params.Add(ApiUrlConstV2.PrLow, searchParams.PriceLow);

            if (searchParams.PriceHigh > 0)
                @params.Add(ApiUrlConstV2.PrHigh, searchParams.PriceHigh);

            //******* END Price ************

            if (!string.IsNullOrEmpty(searchParams.CommunityStatus))
                @params.Add(ApiUrlConstV2.CommStatus, searchParams.CommunityStatus);

            if (searchParams.CommStatusList.Count > 0 && string.IsNullOrEmpty(searchParams.CommunityStatus))
            {
                var ComStatuses = String.Join(",", searchParams.CommStatusList);
                @params.Add(ApiUrlConstV2.CommStatus, ComStatuses);
            }


            if (!string.IsNullOrEmpty(searchParams.SchoolDistrictIds))
                @params.Add(ApiUrlConstV2.DistrictIds, searchParams.SchoolDistrictIds);

            if (searchParams.HotDeals)
                @params.Add(ApiUrlConstV2.HotDeals, 1);

            if (searchParams.SingleFamily)
                @params.Add(ApiUrlConstV2.Sf, 1);
            else if (searchParams.MultiFamily)
                @params.Add(ApiUrlConstV2.Mf, 1);

            if (searchParams.Stories > 0)
                @params.Add(ApiUrlConstV2.Story, searchParams.Stories);

            if (searchParams.NumStory > 0)
                @params.Add(ApiUrlConstV2.NumStory, searchParams.NumStory);

            if (searchParams.MasterBedLocation > 0)
                @params.Add(ApiUrlConstV2.MasterLoc, searchParams.MasterBedLocation);

            if (searchParams.HasRVGarage)
                @params.Add(ApiUrlConstV2.HasRVGarage, searchParams.HasRVGarage);

            if (searchParams.ParentCommId > 0)
                @params.Add(ApiUrlConstV2.ParentCommId, searchParams.ParentCommId);
            else if (searchParams.CommId > 0)
                @params.Add(ApiUrlConstV2.CommId, searchParams.CommId);

            if (searchParams.Comms != null && searchParams.Comms.Any(p => p > 0))
                @params.Add(ApiUrlConstV2.Comms, string.Join(",", searchParams.Comms.Where(p => p > 0)));

            if (searchParams.PlanId > 0)
                @params.Add(ApiUrlConstV2.PlanId, searchParams.PlanId);

            if (searchParams.SpecId > 0)
                @params.Add(ApiUrlConstV2.SpecId, searchParams.SpecId);

            if (searchParams.BrandId > 0)
                @params.Add(ApiUrlConstV2.BrandId, searchParams.BrandId);

            if (searchParams.SqFtLow > 0)
                @params.Add(ApiUrlConstV2.SfLow, searchParams.SqFtLow);

            if (searchParams.SqFtHigh > 0)
                @params.Add(ApiUrlConstV2.SfHigh, searchParams.SqFtHigh);

            if (searchParams.Qmi)
                @params.Add(ApiUrlConstV2.Qmi, 1);

            if (!string.IsNullOrEmpty(searchParams.CommName))
                @params.Add(ApiUrlConstV2.CommName, searchParams.CommName);

            if (searchParams.NoBoyl)
                @params.Add(ApiUrlConstV2.NoBoyl, 1);

            if (searchParams.Cache)
                @params.Add(ApiUrlConstV2.Cache, 1);

            if (searchParams.LastCached)
                @params.Add(ApiUrlConstV2.LastCached, 1);

            if (searchParams.LivingAreas > 0)
                @params.Add(ApiUrlConstV2.Living, searchParams.LivingAreas);

            if (searchParams.ExcludeBasiCommunities)
                @params.Add(ApiUrlConstV2.ExcludeBasiCommunities, 1);

            if (!string.IsNullOrEmpty(searchParams.HomeStatus))
                @params.Add(ApiUrlConstV2.HomeStatus, searchParams.HomeStatus);

            if (searchParams.Builders != null && searchParams.Builders.Any(p => p > 0))
                @params.Add(ApiUrlConstV2.Builders, string.Join(",", searchParams.Builders.Where(p => p > 0)));
            //Generic 

            if (searchParams.CountsOnly)
                @params.Add(ApiUrlConstV2.CountsOnly, 1);

            if (searchParams.SortFacets)
                @params.Add(ApiUrlConstV2.SortFacets, 1);

            if (searchParams.ExtCommDetail)
                @params.Add(ApiUrlConstV2.ExtCommDetail, 1);

            if (searchParams.ExtMapPoints)
                @params.Add(ApiUrlConstV2.ExtMapPoints, 1);

            if (searchParams.ExtHomeDetail)
                @params.Add(ApiUrlConstV2.ExtHomeDetail, 1);

            FillExclude(ref searchParams, @params);

            if (searchParams.BasicListingToTheEnd != null)
                @params.Add(ApiUrlConstV2.BasicListingToTheEnd, searchParams.BasicListingToTheEnd.ToType<int>());

            if (searchParams.MaxReturnRows > 0)
                @params.Add(ApiUrlConstV2.MaxReturnRows, searchParams.MaxReturnRows);


            @params.AddBoolAsInt(ApiUrlConstV2.CustomBuilderLocations, searchParams.CustomBuilderLocations);
            @params.AddBoolAsInt(ApiUrlConstV2.IncludeBrandShowcase, searchParams.IncludeBrandShowcase);
            @params.AddBoolAsInt(ApiUrlConstV2.ShowMarketsFacet, searchParams.ShowMarketsFacet);
            @params.AddBoolAsInt(ApiUrlConstV2.IncludeAgentCompensation, searchParams.IncludeAgentCompensation);
            @params.AddBoolAsInt(ApiUrlConstV2.IncludePromos, searchParams.IncludePromos);
            @params.AddBoolAsInt(ApiUrlConstV2.IncludeEvents, searchParams.IncludeEvents);

            return @params;
        }

        private static void FillExclude(ref SearchParams searchParams, Dictionary<string, object> @params)
        {
            if (searchParams.ExcludeImages)
                @params.Add(ApiUrlConstV2.ExcludeImages, 1);

            if (searchParams.ExcludeVideos)
                @params.Add(ApiUrlConstV2.ExcludeVideos, 1);

            if (searchParams.ExcludeCountsAndFacets)
                @params.Add(ApiUrlConstV2.ExcludeCountsAndFacets, 1);

            if (searchParams.ExcludeSummary)
                @params.Add(ApiUrlConstV2.ExcludeSummary, 1);

            if (searchParams.ExcludeCustomAmenities)
                @params.Add(ApiUrlConstV2.ExcludeCustomAmenities, 1);

            if (searchParams.ExcludeDescription)
                @params.Add(ApiUrlConstV2.ExcludeDescription, 1);

            if (searchParams.ExcludeInteractiveMedia)
                @params.Add(ApiUrlConstV2.ExcludeInteractiveMedia, 1);

            if (searchParams.ExcludeFloorPlans)
                @params.Add(ApiUrlConstV2.ExcludeFloorPlans, 1);

            if (searchParams.ExlucedeFloorPlanViewerUrl)
                @params.Add(ApiUrlConstV2.ExlucedeFloorPlanViewerUrl, 1);

            if (searchParams.ExcludeEnvisionUrl)
                @params.Add(ApiUrlConstV2.ExcludeEnvisionUrl, 1);

            if (searchParams.ExcludeTollFreeNumber)
                @params.Add(ApiUrlConstV2.ExcludeTollFreeNumber, 1);

            if (searchParams.ExcludeSchoolDistricts)
                @params.Add(ApiUrlConstV2.ExcludeSchoolDistricts, 1);

            if (searchParams.ExcludeCommunityMap)
                @params.Add(ApiUrlConstV2.ExcludeCommunityMap, 1);

            if (searchParams.ExcludeHomeOptions)
                @params.Add(ApiUrlConstV2.ExcludeHomeOptions, 1);

            if (searchParams.ExcludeBuilderMap)
                @params.Add(ApiUrlConstV2.ExcludeBuilderMap, 1);

            if (searchParams.ExcludeVideoTour)
                @params.Add(ApiUrlConstV2.ExcludeVideoTour, 1);

            if (searchParams.ExcludeAmenities)
                @params.Add(ApiUrlConstV2.ExcludeAmenities, 1);

            if (searchParams.ExcludePromotions)
                @params.Add(ApiUrlConstV2.ExcludePromotions, 1);

            if (searchParams.ExcludeEvents)
                @params.Add(ApiUrlConstV2.ExcludeEvents, 1);

            if (searchParams.ExcludeAgents)
                @params.Add(ApiUrlConstV2.ExcludeAgents, 1);

            if (searchParams.ExcludeNonPdfBrochure)
                @params.Add(ApiUrlConstV2.ExcludeNonPdfBrochure, 1);

            if (searchParams.ExcludeUtilities)
                @params.Add(ApiUrlConstV2.ExcludeUtilities, 1);

            if (searchParams.ExcludeFeesAndTaxes)
                @params.Add(ApiUrlConstV2.ExcludeFeesAndTaxes, 1);

            @params.AddBoolAsInt(ApiUrlConstV2.ExcludeFacetsOnly, searchParams.ExcludeFacetsOnly);
        }

        public static SearchParams ToClone(this SearchParams searchParams)
        {
            return searchParams.ToJson().ToFromJson<SearchParams>();
        }

        public static ApiSearchParams ToPostApiParameters(this SearchParams searchParams)
        {
            var @params = new ApiSearchParams
            {
                PartnerId = searchParams.PartnerId,
                ExcludeBasicListings = searchParams.ExcludeBasicListings,
                BuilderId = searchParams.BuilderId,
                Bathrooms = searchParams.Bathrooms,
                Bedrooms = searchParams.Bedrooms,
                Garages = searchParams.Garages,
                Pool = searchParams.Pool,
                Green = searchParams.Green,
                NatureAreas = searchParams.NatureAreas,
                GolfCourse = searchParams.GolfCourse,
                Waterfront = searchParams.Waterfront,
                Parks = searchParams.Parks,
                Views = searchParams.Views,
                Sports = searchParams.Sports,
                Adult = searchParams.Adult,
                Gated = searchParams.Gated,
                PriceLow = searchParams.PriceLow,
                PriceHigh = searchParams.PriceHigh,
                CommunityStatus = searchParams.CommunityStatus,
                SchoolDistrictIds = searchParams.SchoolDistrictIds,
                HotDeals = searchParams.HotDeals,
                //SingleFamily = searchParams.SingleFamily,
                //MultiFamily = searchParams.MultiFamily,
                Stories = searchParams.Stories,
                NumStory = searchParams.NumStory,
                MasterBedLocation = searchParams.MasterBedLocation,
                HasRVGarage = searchParams.HasRVGarage,
                CommId = searchParams.CommId,
                ParentCommId = searchParams.ParentCommId,
                PlanId = searchParams.PlanId,
                SpecId = searchParams.SpecId,
                BrandId = searchParams.BrandId,
                SqFtLow = searchParams.SqFtLow,
                SqFtHigh = searchParams.SqFtHigh,
                Qmi = searchParams.Qmi,
                CommName = searchParams.CommName,
                LivingAreas = searchParams.LivingAreas,
                ExcludeBasiCommunities = searchParams.ExcludeBasiCommunities,
                HomeStatus = searchParams.HomeStatus,
                CountsOnly = searchParams.CountsOnly,
                PageSize = searchParams.PageSize,
                PageNumber = searchParams.PageNumber,
                BasicListingToTheEnd = searchParams.BasicListingToTheEnd,
                ExcludeCountsAndFacets = searchParams.ExcludeCountsAndFacets,
                ExtHomeDetail = searchParams.ExtHomeDetail,
                ExtCommDetail = searchParams.ExtCommDetail,
                ExtMapPoints = searchParams.ExtMapPoints,
                SortFacets = searchParams.SortFacets,
                ExcludeImages = searchParams.ExcludeImages,
                ExcludeVideos = searchParams.ExcludeVideos,
                ExcludeSummary = searchParams.ExcludeSummary,
                ExcludeCustomAmenities = searchParams.ExcludeCustomAmenities,
                ExcludeDescription = searchParams.ExcludeDescription,
                ExcludeInteractiveMedia = searchParams.ExcludeInteractiveMedia,
                ExcludeFloorPlans = searchParams.ExcludeFloorPlans,
                ExlucedeFloorPlanViewerUrl = searchParams.ExlucedeFloorPlanViewerUrl,
                ExcludeEnvisionUrl = searchParams.ExcludeEnvisionUrl,
                ExcludeTollFreeNumber = searchParams.ExcludeTollFreeNumber,
                ExcludeSchoolDistricts = searchParams.ExcludeSchoolDistricts,
                ExcludeCommunityMap = searchParams.ExcludeCommunityMap,
                ExcludeHomeOptions = searchParams.ExcludeHomeOptions,
                ExcludeBuilderMap = searchParams.ExcludeBuilderMap,
                ExcludeVideoTour = searchParams.ExcludeVideoTour,
                ExcludeAmenities = searchParams.ExcludeAmenities,
                ExcludePromotions = searchParams.ExcludePromotions,
                ExcludeEvents = searchParams.ExcludeEvents,
                ExcludeAgents = searchParams.ExcludeAgents,
                ExcludeNonPdfBrochure = searchParams.ExcludeNonPdfBrochure,
                ExcludeUtilities = searchParams.ExcludeUtilities,
                ExcludeFeesAndTaxes = searchParams.ExcludeFeesAndTaxes,
                ExcludeFacetsOnly = searchParams.ExcludeFacetsOnly,
                CustomBuilderLocations = searchParams.CustomBuilderLocations
            };

            switch (searchParams.WebApiSearchType)
            {
                case WebApiSearchType.Exact:
                    if (!string.IsNullOrEmpty(searchParams.City))
                        @params.City = searchParams.City;
                    else if (!string.IsNullOrEmpty(searchParams.County))
                        @params.County = searchParams.County;
                    else if (!string.IsNullOrEmpty(searchParams.PostalCode))
                        @params.PostalCode = searchParams.PostalCode;
                    else if (searchParams.Cities != null && searchParams.Cities.Any(p => !string.IsNullOrEmpty(p)))
                        @params.Cities = searchParams.Cities;
                    else if (searchParams.Counties != null && searchParams.Counties.Any(p => !string.IsNullOrEmpty(p)))
                        @params.Counties = searchParams.Counties;
                    else if (searchParams.PostalCodes != null &&
                             searchParams.PostalCodes.Any(p => !string.IsNullOrEmpty(p)))
                        @params.PostalCodes = searchParams.PostalCodes;
                    else if (searchParams.Markets != null && searchParams.Markets.Any(p => p > 0))
                        @params.Markets = searchParams.Markets;
                    else if (searchParams.MarketId > 0)
                        @params.MarketId = searchParams.MarketId;
                    if (!string.IsNullOrEmpty(searchParams.MarketName))
                        @params.MarketName = searchParams.MarketName;

                    if (!string.IsNullOrEmpty(searchParams.State))
                        @params.State = searchParams.State;
                    if (!string.IsNullOrEmpty(searchParams.StateName))
                        @params.StateName = searchParams.StateName;

                    break;
                case WebApiSearchType.Radius:
                    if (searchParams.OriginLat != 0)
                        @params.OriginLat = searchParams.OriginLat;
                    if (searchParams.OriginLng != 0)
                        @params.OriginLng = searchParams.OriginLng;
                    if (searchParams.Radius > 0)
                        @params.Radius = searchParams.Radius;
                    break;
                case WebApiSearchType.Polygon:
                case WebApiSearchType.Map:
                    if (searchParams.MinLat != 0)
                        @params.MinLat = searchParams.MinLat;
                    if (searchParams.MinLng != 0)
                        @params.MinLng = searchParams.MinLng;
                    if (searchParams.MaxLat != 0)
                        @params.MaxLat = searchParams.MaxLat;
                    if (searchParams.MaxLng != 0)
                        @params.MaxLng = searchParams.MaxLng;

                    if (searchParams.WebApiSearchType == WebApiSearchType.Polygon)
                    {
                        @params.ApiGeographyPolygonIntersects = searchParams.ApiGeographyPolygonIntersects;
                    }

                    break;
            }

            #region Promos

            if (searchParams.Promo)
            {
                @params.Promo = searchParams.Promo;
            }
            else
            {
                if (searchParams.ConsumerPromo)
                    @params.ConsumerPromo = searchParams.ConsumerPromo;
                else if (searchParams.AgentPromo)
                    @params.AgentPromo = searchParams.AgentPromo;
            }

            #endregion

            if (searchParams.SortBy == SortBy.Random)
            {
                if (!string.IsNullOrWhiteSpace(searchParams.City))
                    @params.SortBy = SortBy.City;
                else if (!string.IsNullOrWhiteSpace(searchParams.County))
                    @params.SortBy = SortBy.County;
                else if (!string.IsNullOrWhiteSpace(searchParams.PostalCode))
                    @params.SortBy = SortBy.PostalCode;
                else
                    @params.SortBy = SortBy.Random;

                if (!string.IsNullOrEmpty(searchParams.SortFirstBy) &&
                    !string.IsNullOrEmpty(searchParams.City) ||
                    !string.IsNullOrEmpty(searchParams.County) ||
                    !string.IsNullOrEmpty(searchParams.PostalCode))
                {
                    @params.SortFirstBy = searchParams.SortFirstBy;
                    @params.SortSecondBy = SortSecondBy.Random;
                }
            }
            else
                @params.SortBy = searchParams.SortBy;

            if (searchParams.SortOrder && searchParams.SortBy != SortBy.Random)
                @params.SortOrder = searchParams.SortOrder;

            if (searchParams.PlanIdsSort != null && searchParams.PlanIdsSort.Any(p => p > 0))
                @params.PlanIdsSort = searchParams.PlanIdsSort;

            if (searchParams.SpecIdsSort != null && searchParams.SpecIdsSort.Any(p => p > 0))
                @params.SpecIdsSort = searchParams.SpecIdsSort;

            if (searchParams.CommIdsSort != null && searchParams.CommIdsSort.Any(p => p > 0))
                @params.CommIdsSort = searchParams.CommIdsSort;

            if (searchParams.Comms != null && searchParams.Comms.Any(p => p > 0))
                @params.Comms = searchParams.Comms;

            if (searchParams.Builders != null && searchParams.Builders.Any(p => p > 0))
                @params.Builders = searchParams.Builders;

            if (searchParams.Brands != null && searchParams.Brands.Any(p => p > 0))
                @params.Brands = searchParams.Brands;

            if (searchParams.SingleFamily)
                @params.SingleFamily = searchParams.SingleFamily;
            else if (searchParams.MultiFamily)
                @params.MultiFamily = searchParams.MultiFamily;

            if (searchParams.CommStatusList.Count > 0 && string.IsNullOrEmpty(searchParams.CommunityStatus))
            {
                var comStatuses = String.Join(",", searchParams.CommStatusList);
                @params.CommunityStatus = comStatuses;
            }

            if (searchParams.NoBoyl)
                @params.NoBoyl = searchParams.NoBoyl;

            if (searchParams.LastCached)
                @params.LastCached = searchParams.LastCached;

            if (searchParams.Event)
            {
                @params.Event = searchParams.Event;
            }

            return @params;
        }

        public static SearchParams CleanForSave(this SearchParams searchParams)
        {
            if (!searchParams.Counties?.Any() ?? false)
            {
                searchParams.Counties = null;
            }

            if (!searchParams.PostalCodes?.Any() ?? false)
            {
                searchParams.PostalCodes = null;
            }

            if (!searchParams.Cities?.Any() ?? false)
            {
                searchParams.Cities = null;
            }

            if (!searchParams.Builders?.Any(p => p > 0) ?? false)
            {
                searchParams.Builders = null;
            }

            if (!searchParams.Comms?.Any(p => p > 0) ?? false)
            {
                searchParams.Comms = null;
            }

            if (!searchParams.Markets?.Any(p => p > 0) ?? false)
            {
                searchParams.Markets = null;
            }

            if (!searchParams.CommStatusList?.Any() ?? false)
            {
                searchParams.CommStatusList = null;
            }

            if (!searchParams.ApiGeography?.Any() ?? false)
            {
                searchParams.ApiGeography = null;
            }

            if (!searchParams.ResponseParamList?.Any() ?? false)
            {
                searchParams.ResponseParamList = null;
            }

            if (!searchParams.CommIdsSort?.Any(p => p > 0) ?? false)
            {
                searchParams.CommIdsSort = null;
            }

            if (!searchParams.PlanIdsSort?.Any(p => p > 0) ?? false)
            {
                searchParams.PlanIdsSort = null;
            }

            if (!searchParams.SpecIdsSort?.Any(p => p > 0) ?? false)
            {
                searchParams.SpecIdsSort = null;
            }

            if (!searchParams.Brands?.Any(p => p > 0) ?? false)
            {
                searchParams.Brands = null;
            }

            return searchParams;
        }

        //public static void SetFromRouteParam(this SearchParams @params, IList<RouteParam> parameters)
        //{
        //    foreach (var urlParam in parameters)
        //    {
        //        try
        //        {
        //            switch (urlParam.Name)
        //            {
        //                case RouteParams.Adult:
        //                    @params.Adult = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Area:
        //                case RouteParams.MarketName:
        //                    @params.MarketName = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase();
        //                    break;
        //                case RouteParams.BedRooms:
        //                    @params.Bedrooms = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.BathRooms:
        //                    @params.Bathrooms = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.BrandId:
        //                    @params.BrandId = int.Parse(urlParam.Value);
        //                    break;
        //                //case RouteParams.BrandName:
        //                //    @params.BrandName = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase();
        //                //    break;
        //                //case RouteParams.BuilderName:
        //                //    @params.BuilderName = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase();
        //                //    break;
        //                case RouteParams.BuilderId:
        //                    @params.BuilderId = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.City:
        //                case RouteParams.CityNameFilter:
        //                case RouteParams.Ciudad:
        //                    int zipCode;
        //                    if (int.TryParse(urlParam.Value.ToLower().Replace("postalcode", string.Empty).Trim(), out zipCode))
        //                        @params.PostalCode = zipCode.ToString();
        //                    else
        //                    {
        //                        if (!urlParam.Value.StartsWith("refer")) //44797
        //                        {
        //                            @params.City = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase();
        //                            @params.City = @params.City.ReturnSpaceAndDash();

        //                        }
        //                        else
        //                            UserSession.Refer = RouteParams.City.Value<string>().Replace("refer", "").Trim();
        //                    }
        //                    break;
        //                case RouteParams.ComingSoon:
        //                    if (bool.Parse(urlParam.Value))
        //                    {
        //                        @params.ComingSoon = true;
        //                    }
        //                    break;
        //                case RouteParams.CommunityId:
        //                    @params.CommId = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.CommunityName:
        //                    @params.CommName = HttpUtility.UrlDecode(urlParam.Value);
        //                    break;
        //                case RouteParams.CommunityStatus:
        //                    @params.CommunityStatus = urlParam.Value;
        //                    break;
        //                case RouteParams.County:
        //                case RouteParams.Condado:
        //                case RouteParams.CountyNameFilter:
        //                    @params.County = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase().ReturnSpaceAndDash();

        //                    break;
        //                //case UrlConst.HasEvents: paramz.HasEvent = true; break;
        //                case RouteParams.HomeStatus:
        //                    @params.HomeStatus = urlParam.Value.ToUpper();
        //                    break;
        //                case RouteParams.TownHomes:
        //                    if (urlParam.Value.ToType<bool>())
        //                    {
        //                        @params.MultiFamily = true;
        //                        @params.SingleFamily = false;
        //                    }
        //                    break;
        //                case RouteParams.HomeType:
        //                    if (urlParam.Value == "mf")
        //                    {
        //                        @params.MultiFamily = true;
        //                        @params.SingleFamily = false;
        //                    }
        //                    else if (urlParam.Value == "sf")
        //                    {
        //                        @params.MultiFamily = false;
        //                        @params.SingleFamily = true;
        //                    }
        //                    break;
        //                case RouteParams.HotDeals:
        //                    @params.HotDeals = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Inventory:
        //                case RouteParams.SpecHomes:
        //                    @params.Qmi = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Gated:
        //                    @params.Gated = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.GolfCourse:
        //                    @params.GolfCourse = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Green:
        //                case RouteParams.GreenProgram:
        //                    @params.Green = bool.Parse(urlParam.Value);
        //                    break;
        //                //case RouteParams.ListingType: 
        //                //    paramz.ListingType = urlParam.Value; break;
        //                case RouteParams.Market:
        //                case RouteParams.MarketId:
        //                    @params.MarketId = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.MasterBedLocation:
        //                    @params.MasterBedLocation = urlParam.Value.ToType<int>();
        //                    break;
        //                case RouteParams.HasRVGarage:
        //                    @params.HasRVGarage = urlParam.Value.ToType<bool>();
        //                    break;
        //                case RouteParams.MaxLat:
        //                    @params.MaxLat = double.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.MaxLng:
        //                    @params.MaxLng = double.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.MinLat:
        //                    @params.MinLat = double.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.MinLng:
        //                    @params.MinLng = double.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.NatureAreas:
        //                    @params.NatureAreas = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.NumOfBaths:
        //                    @params.Bathrooms = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.NumOfGarages:
        //                    @params.Garages = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.NumOfLiving:
        //                    @params.LivingAreas = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.OriginLat:
        //                    @params.OriginLat = double.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.OriginLong:
        //                    @params.OriginLng = double.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Page:
        //                    @params.PageNumber = urlParam.Value.ToType<int>() > 0 ? urlParam.Value.ToType<int>() : 1;
        //                    break;
        //                case RouteParams.Parks:
        //                    @params.Parks = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.QuickMoveIn:
        //                    @params.Qmi = bool.Parse(urlParam.Value);
        //                    @params.HomeStatus = "A,UC";
        //                    break;
        //                case RouteParams.Pool:
        //                    @params.Pool = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Postal:
        //                case RouteParams.PostalCode:
        //                case RouteParams.PostalCodeFilter:
        //                    @params.PostalCode = urlParam.Value;
        //                    break;
        //                case RouteParams.PriceHigh:
        //                    @params.PriceHigh = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.PriceLow:
        //                    @params.PriceLow = int.Parse(urlParam.Value);
        //                    break;

        //                case RouteParams.HasEvents:
        //                    @params.Event = urlParam.Value.ToType<bool>();
        //                    break;
        //                case RouteParams.PromotionType:
        //                    if (urlParam.Value == PromotionType.Both.ToString().ToLower())
        //                        @params.Promo = true;
        //                    else if (urlParam.Value == PromotionType.Agent.ToString().ToLower())
        //                        @params.AgentPromo = true;
        //                    else if (urlParam.Value == PromotionType.Consumer.ToString().ToLower())
        //                        @params.ConsumerPromo = true;
        //                    break;
        //                case RouteParams.SchoolDistrictId:
        //                    @params.SchoolDistrictIds = urlParam.Value;
        //                    break;
        //                case RouteParams.SchoolDistrictName:
        //                    @params.SchoolDistrictName = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase();
        //                    break;
        //                case RouteParams.SportsFacilities:
        //                    @params.Sports = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.SquareFeet:
        //                case RouteParams.SqFtLow:
        //                    @params.SqFtLow = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.SqFtHigh:
        //                    @params.SqFtHigh = int.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.State:
        //                    @params.State = HttpUtility.UrlDecode(urlParam.Value);
        //                    break;
        //                case RouteParams.StateName:
        //                    @params.StateName = HttpUtility.UrlDecode(urlParam.Value).ToTitleCase().ReturnSpaceAndDash();
        //                    break;
        //                case RouteParams.Stories:
        //                    @params.Stories = decimal.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.Views:
        //                    @params.Views = bool.Parse(urlParam.Value);
        //                    break;
        //                case RouteParams.WaterFront:
        //                    @params.Waterfront = bool.Parse(urlParam.Value);
        //                    break;
        //            }
        //        }
        //        catch
        //        {
        //            // Error trying to assign value to search parameter.  Ignore error and use parameter default value. 
        //        }
        //    }
        //}

        public static bool HaveSchoolDistricts(this SearchParams searchParams)
        {
            var isNull = string.IsNullOrEmpty(searchParams.SchoolDistrictIds);
            if (isNull)
                return false;
            var ammountOfSchoolDistricts = searchParams.SchoolDistrictIds.Split(',');
            return ammountOfSchoolDistricts.Length > 0;
        }

        public static int SchoolDistrictsCount(this SearchParams searchParams)
        {
            var isNull = string.IsNullOrEmpty(searchParams.SchoolDistrictIds);
            if (isNull)
                return 0;
            var ammountOfSchoolDistricts = searchParams.SchoolDistrictIds.Split(',');
            return ammountOfSchoolDistricts.Length;
        }

        public static int FirstSchoolDistrict(this SearchParams searchParams)
        {
            var isNull = string.IsNullOrEmpty(searchParams.SchoolDistrictIds);
            if (isNull)
                return 0;
            var ammountOfSchoolDistricts = searchParams.SchoolDistrictIds.Split(',');
            if (ammountOfSchoolDistricts.Length > 0)
            {
                return searchParams.SchoolDistrictIds.Split(',')[0].ToType<int>();
            }
            return 0;
        }

        /// <summary>
        /// According search parameters we will get the text that we are looking for
        /// 
        /// The order of this is the following:
        /// 
        /// First Market --> Austin, TX Area
        /// City --> Austin, TX
        /// County
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public static string GetSearchText(this SearchParams searchParameters)
        {
            var text = string.Empty;
            if (searchParameters.MarketId > 0 && !string.IsNullOrEmpty(searchParameters.MarketName))
            {
                text = searchParameters.MarketName + ", " + searchParameters.State + " Area";
            }

            if (!string.IsNullOrEmpty(searchParameters.City))
            {
                text = searchParameters.City + ", " + searchParameters.State;
            }

            if (!string.IsNullOrEmpty(searchParameters.County))
            {
                text = searchParameters.County + " County" + ", " + searchParameters.State;
            }

            if (!string.IsNullOrEmpty(searchParameters.PostalCode) && searchParameters.PostalCode.ToType<int>() > 0)
            {
                text = searchParameters.PostalCode + ", " + searchParameters.State;
            }

            if (searchParameters.HaveSchoolDistricts())
            {
                var schoolsLength = searchParameters.SchoolDistrictsCount();
                if (schoolsLength > 1)
                    text = schoolsLength + " School Districts in " + text;
                else
                {

                    var location = !string.IsNullOrEmpty(searchParameters.City)
                        ? searchParameters.City
                        : searchParameters.MarketName;
                    text = searchParameters.SchoolDistrictName + " in " + location + ", " +
                           searchParameters.State + " Area";
                }
            }

            return text;
        }

        public static string ToLocationTextSrp(this SearchParams searchParameters)
        {

            var text = "";
            if (searchParameters.MarketId > 0)
                text = searchParameters.MarketName + ", " + searchParameters.State + " Area";

            if (!string.IsNullOrEmpty(searchParameters.City))
                text = searchParameters.City + ", " + searchParameters.State;

            if (!string.IsNullOrEmpty(searchParameters.County))
                text = searchParameters.County + " County, " + searchParameters.State;

            if (!string.IsNullOrEmpty(searchParameters.PostalCode))
                text = searchParameters.PostalCode + ", " + searchParameters.State;

            if (searchParameters.Cities != null)
            {
                if (searchParameters.Cities.Count > 1)
                    text = searchParameters.Cities.Count + " Cities in " + text;
            }

            if (searchParameters.PostalCodes != null)
            {
                if (searchParameters.PostalCodes.Count > 1)
                    text = searchParameters.PostalCodes.Count + " Zip Codes in " + text;
            }
            if (searchParameters.Counties != null)
            {
                if (searchParameters.Counties.Count > 1)
                    text = searchParameters.Counties.Count + " Counties in " + text;
            }
            if (searchParameters.HaveSchoolDistricts())
            {
                var schoolsLength = searchParameters.SchoolDistrictsCount();
                if (schoolsLength > 1)
                    text = schoolsLength + " School Districts in " + text;
                else
                {
                    var location = searchParameters.LocationType.Type == 10
                        ? searchParameters.City
                        : searchParameters.MarketName;
                    text = searchParameters.SchoolDistrictName + " in " + location + ", " +
                           searchParameters.State + " Area";
                }
            }

            if (searchParameters.IsSearchWithMap)
                text = "this area";

            return text;
        }

        public static string GetSearchText(this Location location)
        {
            var text = string.Empty;
            var locationType = location.Type.FromInt<LocationType>();
            switch (locationType)
            {
                case LocationType.Market:
                    text = location.Name + ", " + location.State + " Area";
                    break;
                case LocationType.City:
                    text = location.Name + ", " + location.State;
                    break;
                case LocationType.Zip:
                case LocationType.Community:
                    text = location.Name + " in " + location.City + ", " + location.State;
                    break;
                case LocationType.Developer:
                    text = location.Name + ", " + location.State;
                    break;
                case LocationType.School:
                    text = location.Name + " in " + location.City + ", " + location.State;
                    break;
                case LocationType.SchoolSrp:
                    text = location.Name + " in " + location.MarketName + ", " + location.State + " Area";
                    break;

                case LocationType.County:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return text;
        }

        public static void AddCity(this SearchParams searchParameters, string city, string stateAbbr)
        {
            var cityName = city + "," + stateAbbr;
            if (!searchParameters.Cities.Contains(cityName))
                searchParameters.Cities.Add(cityName);
        }

        /// <summary>The SearchParams extension method that converts this object to a search parameters
        /// for community.</summary>
        /// <param name="partnerId">       Identifier for the partner.</param>
        /// <param name="communityId">     Id of the community.</param>
        /// <param name="excludePromos">    (Optional) True to exclude, false to include the promos.</param>
        /// <returns>The given data converted to the SearchParams.</returns>
        public static SearchParams ToSearchParamsForCommunity(this SearchParams searchParameters, int partnerId, int communityId, bool excludePromos = true)
        {
            var searchParams = new SearchParams
            {
                PartnerId = partnerId,
                CommId = communityId,
                WebApiSearchType = WebApiSearchType.Exact,
                ExcludeImages = true,
                ExcludeVideos = true,
                ExcludePromotions = excludePromos,
                ExcludeTollFreeNumber = true,
                ExcludeCustomAmenities = true,
                ExcludeInteractiveMedia = true,
                ExcludeVideoTour = true,
                ExcludeEvents = true,
                ExcludeCommunityMap = true,
                ExcludeBuilderMap = true,
                ExcludeNonPdfBrochure = true,
                ExlucedeFloorPlanViewerUrl = true,
            };
            return searchParams;
        }

        /// <summary>The SearchParams extension method that converts this object to a search parameters
        /// for home.</summary>
        /// <param name="searchParameters">Parameters from the search.</param>
        /// <param name="partnerId">       Identifier for the partner.</param>
        /// <param name="homeId">          Identifier for the home.</param>
        /// <param name="isSpec">          True if is specifier, false if not.</param>
        /// <param name="includeVideoUrls"> (Optional) True to include, false to exclude the video urls.</param>
        /// <param name="includeSummaries"> (Optional) True to include, false to exclude the summaries.</param>
        /// <returns>The given data converted to the SearchParams.</returns>
        public static SearchParams ToSearchParamsForHome(this SearchParams searchParameters, int partnerId, int homeId, bool isSpec, bool includeVideoUrls = false, bool includeSummaries = false)
        {
            searchParameters.ExcludeBuilderMap = true;
            searchParameters.ExcludeCommunityMap = true;
            searchParameters.ExcludeCustomAmenities = true;
            searchParameters.ExcludeEnvisionUrl = true;
            searchParameters.ExcludeEvents = true;
            searchParameters.ExcludeImages = true;
            searchParameters.ExcludeInteractiveMedia = true;
            searchParameters.ExcludeNonPdfBrochure = true;
            searchParameters.ExcludeSummary = !includeSummaries;
            searchParameters.ExcludeTollFreeNumber = true;
            searchParameters.ExcludeVideoTour = true;
            searchParameters.ExcludeVideos = true;
            searchParameters.ExlucedeFloorPlanViewerUrl = true;
            searchParameters.PartnerId = partnerId;

            if (isSpec)
            {
                searchParameters.SpecId = homeId;
            }
            else
            {
                searchParameters.PlanId = homeId;
            }

            return searchParameters;
        }

        /// <summary>This will create the base Search Parameters for the BuilderShowcase Page.</summary>
        /// <remarks>CeLopez, 2/27/2019.</remarks>
        /// <param name="partnerId">       Identifier for the partner.</param>
        /// <param name="brandId">               Id of the corresponding Builder  Showcase.</param>
        /// <param name="pageSize">                 (Optional) Amount of results you want to retrieve.</param>
        /// <param name="excludeFacetsOnly">        (Optional) Boolean to define if we should exclude the
        ///                                         facets.</param>
        /// <param name="excludeFacetsAndCounts">(Optional) True to exclude Facets and Counts.</param>
        /// <returns>The base builder showcase search parameters.</returns>
        public static SearchParams CreateBaseBuilderShowcaseSearchParams(int partnerId, int brandId, int pageSize = 10, bool excludeFacetsOnly = true, bool excludeFacetsAndCounts = true)
        {
            return new SearchParams
            {
                BrandId = brandId,
                ExcludeCountsAndFacets = excludeFacetsAndCounts,
                ExcludeFacetsOnly = excludeFacetsOnly,
                PageSize = pageSize,
                PartnerId = partnerId,
                WebApiSearchType = WebApiSearchType.Exact,
            };
        }
    }
}