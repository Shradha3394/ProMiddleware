using Pro.Api.Model.Concrete;

namespace Pro.Api.Repository.Abstract;
public interface IPartnerRepository
{
    List<Partner> GetPartnerTableByBrandPartner(int brandPartnerId);

}