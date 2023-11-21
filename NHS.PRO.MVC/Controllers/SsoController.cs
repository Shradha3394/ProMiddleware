using Microsoft.AspNetCore.Mvc;
using NHS.PRO.MVC.Models;
using System.Diagnostics;
using ComponentSpace.Saml2;

namespace NHS.PRO.MVC.Controllers
{
    [Route("armls/sso")]
    public class SsoController : Controller
    {
        private readonly ISamlServiceProvider _samlServiceProvider;

        public SsoController(ISamlServiceProvider samlServiceProvider)
        {
            // Initialize the SamlServiceProvider with your specific configuration.
            _samlServiceProvider = samlServiceProvider;
        }

        [HttpGet]
        [Route("startsso")]
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
            await _samlServiceProvider.InitiateSsoAsync(partnerName, returnUrl, null);

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
            }
            catch (Exception ex)
            {
                // Log or inspect the exception details here.
                // ex.Message, ex.InnerException, etc.
                Console.WriteLine(ex.Message);
            }
            return new EmptyResult();

        }

    }
}