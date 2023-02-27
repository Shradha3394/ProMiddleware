using Pro.Api.Model.Concrete;
using Pro.Api.Repository.Abstract;

namespace Pro.Api.Repository.Concrete;
public class PartnerRepository : IPartnerRepository
{
    public List<Partner> GetPartnerTableByBrandPartner(int brandPartnerId)
    {
        return new List<Partner>
        {
            new() { PartnerId = 88, PartnerName = "NHS Pro", PrivateLabelSite = "" },
            new() { PartnerId = 89, PartnerName = "prodemo", PrivateLabelSite = "prodemo", PartnerLogo = "https://resources.newhomesourceprofessional.com/globalresources/default/images/partners/8640_logo.gif" }
        };
    }
}
