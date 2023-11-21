using Pro.Api.Model;
using Pro.Api.Model.Concrete;

namespace Pro.Api.Service.Services.Abstract;

public interface ISsoService
{
    bool RequestLoginAtIdentityProvider(string partnerSiteUrl, string partnerSsoDestinationUrl);
}