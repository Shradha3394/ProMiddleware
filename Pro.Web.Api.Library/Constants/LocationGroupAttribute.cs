using Pro.Web.Api.Library.Constants.Enums;

namespace Pro.Web.Api.Library.Constants
{
    public class LocationGroupAttribute : Attribute, IComparable
    {
        public LocationGroup LocationGroup { get; set; }
        public string GroupTitle { get; set; }
        public string GroupTitlePlurar { get; set; }

        public LocationGroupAttribute(LocationGroup locationGroup)
        {
            LocationGroup = locationGroup;
        }

        public LocationGroupAttribute(LocationGroup locationGroup, string groupTitle)
            : this(locationGroup, groupTitle, groupTitle)
        {
        }

        public LocationGroupAttribute(LocationGroup locationGroup, string groupTitle, string groupTitlePlurar)
            : this(locationGroup)
        {
            GroupTitle = groupTitle;
            GroupTitlePlurar = groupTitlePlurar;
        }

        public int CompareTo(object obj)
        {
            return obj.GetHashCode();
        }
    }
}
