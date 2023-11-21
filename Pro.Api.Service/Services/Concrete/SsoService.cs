using Pro.Api.Model;
using Pro.Api.Model.Concrete;
using Pro.Api.Repository.Abstract;
using Pro.Api.Service.Services.Abstract;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Pro.Api.Service.Services.Concrete
{
    public class SsoService : ISsoService
    {
        public bool RequestLoginAtIdentityProvider(string partnerSiteUrl, string partnerSsoDestinationUrl)
        {
            throw new NotImplementedException();
        }
    }

}
