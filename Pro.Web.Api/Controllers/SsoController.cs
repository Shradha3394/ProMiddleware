using ComponentSpace.Saml2;
using ComponentSpace.Saml2.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Pro.Api.Model.Concrete;
using Pro.Api.Service.Services.Abstract;

namespace Pro.Web.Api.Controllers
{
    [ApiController]
    [Route("armls/[controller]")]
    public class SsoController : ControllerBase
    {
        private readonly ISsoService _ssoService;
        private readonly IPartnerService _partnerService;
        private readonly ISamlServiceProvider _samlServiceProvider;

        public SsoController(ISsoService ssoService, IPartnerService partnerService, ISamlServiceProvider samlServiceProvider)
        {
            _ssoService = ssoService;
            _partnerService = partnerService;
            _samlServiceProvider = samlServiceProvider;
        }

        [HttpGet]
        [Route("StartSso")]
        public async Task<IActionResult> InitiateSingleSignOn()
        {
            
            var returnUrl = "https://www.newhomesourceprofessional.com/armls/SSO/AssertionConsumerService?binding=post";
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            /*  if (!IsWhitelisted(returnUrl))
              {
                  return BadRequest();
              }*/

            // To login automatically at the service provider, initiate single sign-on to the identity provider (SP-initiated SSO).            
            //   var partnerName = _configuration["PartnerName"];
            var partnerName = "https://auth.armls.com";
            await _samlServiceProvider.InitiateSsoAsync(partnerName, returnUrl);
         
            return new EmptyResult();
        }
        [HttpPost]
        [Route("AssertionConsumerService")]
        public async Task<IActionResult> AssertionConsumerService()
        {
            string samlResponse = HttpContext.Request.Form["SAMLResponse"];
            //var samlResponseObj = new ComponentSpace.Saml2.Respo
            // Receive and process the SAML assertion contained in the SAML response.
            // The SAML response is received either as part of IdP-initiated or SP-initiated SSO.
            try
            {
                var ssoResult = await _samlServiceProvider.ReceiveSsoAsync();
                Console.WriteLine(ssoResult);
                var ssoAttributes = ssoResult.Attributes.Where(xSamlAttribute => xSamlAttribute.Name != null);
            }
            catch (SamlProtocolException ex)
            {
                // Log or inspect the exception details here.
                // ex.Message, ex.InnerException, etc.
                Console.WriteLine(ex.Message);
            }
            

            // Create and save a JWT as a cookie.
          /*  var jwt = new JwtSecurityTokenHandler().WriteToken(CreateJwtSecurityToken(ssoResult));

            Response.Cookies.Append(_configuration["JWT:CookieName"], jwt, _cookieOptions);

            // Redirect to the specified URL.
            if (!string.IsNullOrEmpty(ssoResult.RelayState))
            {
                if (!IsWhitelisted(ssoResult.RelayState))
                {
                    return BadRequest();
                }

                return Redirect(ssoResult.RelayState);
            }*/

            return new EmptyResult();
        }


    }
}