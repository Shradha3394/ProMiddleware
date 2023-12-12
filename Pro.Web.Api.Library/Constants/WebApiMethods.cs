namespace Pro.Web.Api.Library.Constants
{
    public class WebApiMethods
    {
        public const string TypeaheadElasticLocations = "typeahead/elasticsearchlocations";
        public const string Recommendations = "search/recommendations";
        public const string CommunityLocations = "search/communitylocations";
        public const string HomeLocations = "search/homelocations";
        public const string Communities = "search/communities";
        public const string Homes = "search/homes";
        public const string Builders = "search/builders";
        public const string Brands = "search/brands";
        public const string Community = "detail/community";
        public const string CommunityMedia = "media/community";
        public const string HomeMedia = "media/home";
        public const string Home = "detail/home";
        public const string GraphQl = "graphql";
        public const string SearchAllHomes = "searchAllHomes";
        public const string SearchAllHomesCounts = "searchAllHomesCounts";
        public const string SearchAllHomesFacets = "searchAllHomesFacets";
        public const string AddPreferred = "addPreferred";
        public const string DeletePreferred = "deletePreferred";
        public const string SearchAllPreferred = "searchAllPreferred";
        public const string CommunityTestimonials = "detail/testimonials";
        public const string CommunityEventsAndPromotions = "detail/eventsandpromotions";
        public const string PostLeads = "Leads/PostLeads";


        public const string SavedSearchesCreate = "savedsearches/create";
        public const string SavedSearchesUpdate = "savedsearches/update";
        public const string SavedSearchesUpdateLastView = "savedsearches/updatelastview";
        public const string SavedSearchesUserList = "savedsearches/userlist";
        public const string SavedSearchesAgentClientCount = "savedsearches/agentclientlistcounts";
        public const string SavedSearchesGetClientCounts = "savedsearches/getclientcounts";
        public const string SavedSearchesClientList = "savedsearches/clientlist";
        public const string SavedSearchesAgentClientList = "savedsearches/agentclientlist";
        public const string SavedSearchesGetAgentAndClientCounts = "savedsearches/getagentandclientcounts";


        public const string SavedSearchesClientsByAgentList = "savedsearches/clientsbyagentlist";
        public const string SavedSearchesDelete = "savedsearches/delete";
        public const string SavedSearches = "savedsearches/savedsearches";
        public const string SavedSearchesUpdateEmailSend = "savedsearches/updateemailsend";
        public const string ShowSavedSearchToClient = "savedsearches/showsavedsearchtoclient";

        public const string LikesDelete = "likes/delete";
        public const string LikesCreate = "likes/create";
        public const string LikesUpdate = "likes/update";
        public const string LikesClientDelete = "likes/ClientDelete";
        public const string LikesUpdateArchived = "likes/UpdateArchived";

        public const string FeedBackGetByProperty = "feedback/getbyproperty";
        public const string FeedBackOnlyAgent = "feedback/onlyagent";
        public const string FeedBackByAgentAndClient = "feedback/byagentandclient";
        public const string FeedBackGetClientCountsByAgent = "feedback/getclientcountsbyagent";
        public const string FeedBackGetCountsByClient = "feedback/getcountsbyclient";


        public const string ClientFeedBacksByAgent = "feedback/clientfeedbacksbyagent";

        public const string CommentsCreate = "comments/create";
        public const string CommentsUpdate = "comments/update";
        public const string CommentsClientDelete = "comments/clientdelete";
        public const string CommentsUpdateArchived = "comments/UpdateArchived";


        public const string GetAgentComments = "comments/onlyagent";
        public const string GetCommentsByAgentAndClient = "comments/byagentandclient";
        public const string CommentsByProperty = "comments/byproperty";

        public const string SendEmail = "emailservices/sendemail";
        public const string EmailServices = "emailservices/sendemail";
        public const string EventLogger = "log/eventlogger";

        /// <summary>The markets.</summary>
        public const string Markets = "detail/markets";

        /// <summary>The media brand.</summary>
        public const string MediaBrand = "media/brand";

        public const string AgentCompensation = "agentcompensation/getagentcompensation";
        public const string GetCommunityIspDetails = "api/Community/GetCommunityISPDetails";

    }
}
