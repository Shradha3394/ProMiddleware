//using System.Web;
//using Nhs.Utility.Common;
//using Pro.Api.Model.Concrete;
//using Pro.Api.Model.Constants;
//using Pro.Api.Service.Services.Abstract;

//namespace Pro.Api.Service.Services.Concrete
//{
//    internal class LocationService:ILocationService
//    {
//        public string GetLocation(Location location, int partnerId)
//        {
//            var hadAreaWord = false;
//            try
//            {
               
//                location.SearchText = TypeAheadAbbreviations.ReplaceStateName(location.SearchText.ToLower().Trim()).ToLower();

//                if (location.SearchText.EndsWith("area", StringComparison.InvariantCultureIgnoreCase))
//                {
//                    location.SearchText =
//                        location.SearchText.Substring(0,
//                                location.SearchText.LastIndexOf("area", StringComparison.InvariantCultureIgnoreCase))
//                            .Trim();
//                    location.SearchText = ((int)LocationType.Market).ToType<string>();
//                    hadAreaWord = true;
//                }
//                else if (location.Type.ToType<int>() == (int)LocationType.Market)
//                    location.Type = string.Empty;



//                if (location.SearchText.IndexOf(",", StringComparison.Ordinal) == -1 && !hadAreaWord)
//                    // if not of the form Location, State it wast selected to from the type ahead list, so reset search type                
//                    location.Type = string.Empty;

//                var locations = _typeaheadService.GetTypeAheadOptions(location.SearchText, partnerId);

//                if (locations.Count == 1)
//                {
//                    return LocationHanlderRedirect(locations.FirstOrDefault());
//                }

//                var state = string.Empty;

//                if (searchText.LastIndexOf(" ", StringComparison.Ordinal) != -1)
//                    state = location.SearchText.Substring(location.SearchText.LastIndexOf(" ", StringComparison.Ordinal)).Trim();

//                var stateAbbr = string.Empty;

//                if (!string.IsNullOrEmpty(state))
//                {
//                    if (state.Length == 2)
//                        stateAbbr = string.IsNullOrEmpty(_stateService.GetStateName(state)) ? null : state;
//                    else
//                        stateAbbr = _stateService.GetStateAbbreviation(state);
//                }

//                // Searching by Zip
//                if (CommonUtils.IsZip(searchText))
//                {
//                    param.Add(new RouteParam(RouteParams.SearchText, searchText, RouteParamType.QueryString));
//                }
//                // Searching by City, ST or Market, ST
//                else if (searchText.IndexOf(",", StringComparison.Ordinal) != -1 || !string.IsNullOrEmpty(stateAbbr))
//                {
//                    int commaPostion = searchText.IndexOf(",", StringComparison.Ordinal);
//                    string locationText, stateText;

//                    if (commaPostion != -1)
//                    {
//                        locationText = location.SearchText.Substring(0, commaPostion).Trim();
//                        stateText = location.SearchText.Substring(commaPostion + 1).Trim();
//                    }
//                    else
//                    {
//                        locationText = location.SearchText.Substring(0, searchText.LastIndexOf(" ", StringComparison.Ordinal));
//                        stateText = stateAbbr;
//                    }

//                    if (!string.IsNullOrEmpty(locationText) && !string.IsNullOrEmpty(stateText))
//                    {
//                        // Full State Name
//                        if (stateText.Length > 2)
//                        {
//                            stateText = _stateService.GetStateAbbreviation(stateText);
//                        }


//                        if (CommonUtils.IsZip(locationText) && string.IsNullOrWhiteSpace(stateText) == false)
//                        {
//                            var marketFromZip = _marketService.GetMarketIdFromPostalCode(locationText,
//                                partnerId);
//                            var marketfromId = _marketService.GetMarket(partnerId, marketFromZip, false);
//                            if (marketfromId != null && stateText.ToLower() != marketfromId.StateAbbr.ToLower())
//                            {
//                                locationText = locationText + " " + stateText;
//                                stateText = string.Empty;
//                            }
//                        }


//                        if (string.IsNullOrWhiteSpace(stateText) == false)
//                        {
//                            param.Add(new RouteParam(RouteParams.State, stateText));
//                            UserSession.PersonalCookie.State = stateText;
//                        }

//                        UserSession.PersonalCookie.SearchText = location.SearchText;
//                    }
//                    else
//                    {
//                        ModelState.AddModelError("SearchText", @"invalid data, location and state required.");
//                    }
//                }
//                else
//                {
//                    param.Add(new RouteParam(RouteParams.SearchText, searchText, RouteParamType.QueryString));
//                    UserSession.PersonalCookie.SearchText = searchText;
//                }


//                if (!ModelState.IsValid && !ProRoute.ShowMobileSite)
//                {
//                    var vm = GetViewModel(searchText);
//                    // ReSharper disable Asp.NotResolved
//                    return View(vm);
//                    // ReSharper restore Asp.NotResolved
//                }

//                // Rules for prices 
//                if (!string.IsNullOrEmpty(priceLow) || !string.IsNullOrEmpty(priceHigh))
//                {
//                    if (!string.IsNullOrEmpty(priceLow) && priceLow.IndexOf("$", StringComparison.Ordinal) != -1)
//                        priceLow = priceLow.Substring(1);

//                    if (!string.IsNullOrEmpty(priceHigh) && priceHigh.IndexOf("$", StringComparison.Ordinal) != -1)
//                        priceHigh = priceHigh.Substring(1);

//                    if (!string.IsNullOrEmpty(priceLow) && priceLow.IndexOf(",", StringComparison.Ordinal) != -1)
//                        priceLow = priceLow.Replace(",", string.Empty);

//                    if (!string.IsNullOrEmpty(priceHigh) && priceHigh.IndexOf(",", StringComparison.Ordinal) != -1)
//                        priceHigh = priceHigh.Replace(",", string.Empty);

//                    if (!string.IsNullOrEmpty(priceLow) && priceLow != "0" && priceLow.Length < 4)
//                        priceLow = priceLow + "000";

//                    if (!string.IsNullOrEmpty(priceHigh) && priceHigh != "0" && priceHigh.Length < 4)
//                        priceHigh = priceHigh + "000";

//                    if (priceLow == "0") priceLow = null;
//                    if (priceHigh == "0") priceHigh = null;

//                    if (!string.IsNullOrEmpty(priceHigh) && !string.IsNullOrEmpty(priceLow) &&
//                        priceLow.ToType<int>() > priceHigh.ToType<int>())
//                    {
//                        var temp = priceLow;
//                        priceLow = priceHigh;
//                        priceHigh = temp;
//                    }
//                }

//                if (squareFeet > 0)
//                {
//                    param.Add(new RouteParam(RouteParams.SqFtLow, squareFeet, RouteParamType.QueryString));
//                }

//                if (bathrooms > 0)
//                {
//                    param.Add(new RouteParam(RouteParams.BathRooms, bathrooms, RouteParamType.QueryString));
//                }

//                if (bedRooms > 0)
//                {
//                    param.Add(new RouteParam(RouteParams.BedRooms, bedRooms, RouteParamType.QueryString));
//                }

//                if (quickMoveIn > 0)
//                {
//                    param.Add(new RouteParam(RouteParams.QuickMoveIn, quickMoveIn, RouteParamType.QueryString));
//                }

//                if (!string.IsNullOrEmpty(priceLow))
//                {
//                    param.Add(new RouteParam(RouteParams.PriceLow, priceLow, RouteParamType.QueryString));
//                }

//                if (!string.IsNullOrEmpty(priceHigh))
//                {
//                    param.Add(new RouteParam(RouteParams.PriceHigh, priceHigh, RouteParamType.QueryString));
//                }

//                if (!string.IsNullOrEmpty(searchType))
//                    param.Add(new RouteParam(RouteParams.SearchType, searchType, RouteParamType.QueryString));

//                foreach (string item in collection.Keys)
//                {
//                    var parameter = item.FromString<RouteParams>();
//                    var value = collection[item];
//                    param.Add(new RouteParam(parameter, value, RouteParamType.QueryString));
//                }
//            }
//            catch (Exception ex)
//            {
//                ErrorLogger.Log($"getting error on form collection:- searchText:{searchText} searchType:{searchType} priceLow:{priceLow} priceHigh:{priceHigh} squareFeet:{squareFeet} bedrooms:{bedRooms} bathroom:{bathrooms} quickMoveIn:{quickMoveIn} ", ex);
//            }

//            throw new NotImplementedException();
//        }

//        private static List<RouteParam> GetFilterUrlParams()
//        {
//            var urlParams = new List<RouteParam>();
//            if (RouteParams.PriceLow.Value<int>() > 0)
//            {
//                urlParams.Add(RouteParams.PriceLow.ToRouteParam(RouteParamType.QueryString));
//            }

//            if (RouteParams.PriceHigh.Value<int>() > 0)
//            {
//                urlParams.Add(RouteParams.PriceHigh.ToRouteParam(RouteParamType.QueryString));
//            }

//            if (RouteParams.BathRooms.Value<int>() > 0)
//            {
//                urlParams.Add(RouteParams.BathRooms.ToRouteParam(RouteParamType.QueryString));
//            }

//            if (RouteParams.BedRooms.Value<int>() > 0)
//            {
//                urlParams.Add(RouteParams.BedRooms.ToRouteParam(RouteParamType.QueryString));
//            }

//            if (RouteParams.SqFtLow.Value<int>() != 0)
//            {
//                urlParams.Add(RouteParams.SqFtLow.ToRouteParam(RouteParamType.QueryString));
//            }

//            if (RouteParams.QuickMoveIn.Value<int>() > 0)
//                urlParams.Add(new RouteParam(RouteParams.QuickMoveIn, "true", RouteParamType.QueryString));
//            return urlParams;
//        }

//    }
//}
