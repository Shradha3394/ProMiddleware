using Pro.Api.Model;
using Pro.Api.Model.Concrete;
using Pro.Api.Repository.Abstract;
using Pro.Api.Service.Services.Abstract;
namespace Pro.Api.Service.Services.Concrete;

public class PartnerService : IPartnerService
{
    private readonly IPartnerRepository _partnerRepository;

    public PartnerService(IPartnerRepository partnerRepository)
    {
        _partnerRepository = partnerRepository;
    }

    public List<Partner> GetPartnersTable()
    {
       return _partnerRepository.GetPartnerTableByBrandPartner(88);
    }
}