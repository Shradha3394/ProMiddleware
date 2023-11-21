using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Api.Model.Constants
{
    public enum LocationType
    {
        [LocationGroup(LocationGroup.Market, "City", "Cities")]
        Market = 1,
        [LocationGroup(LocationGroup.Market, "City", "Cities")]
        City = 2,
        [LocationGroup(LocationGroup.County, "County", "Counties")]
        County = 3,
        [LocationGroup(LocationGroup.Zip)]
        Zip = 4,
        [LocationGroup(LocationGroup.CommunityDetail, "Community", "Communities")]
        Community = 5,
        [LocationGroup(LocationGroup.Developer)]
        Developer = 6,
        [LocationGroup(LocationGroup.School, "School", "Schools")]
        School = 10,
        [LocationGroup(LocationGroup.School, "School", "Schools")]
        SchoolSrp = 11
    }
}
