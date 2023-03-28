using Pro.Api.Model;

namespace Pro.Api.Repository.Abstract;
public interface IPartnerRepository
{
    List<Partner> GetPartnerTableByBrandPartner(int brandPartnerId);

}