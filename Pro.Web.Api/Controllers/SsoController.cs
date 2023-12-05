using ComponentSpace.Saml2;
using ComponentSpace.Saml2.Assertions;
using ComponentSpace.Saml2.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Nhs.Utility.Common;
using Pro.Api.Model.Concrete;
using Pro.Api.Service.Services.Abstract;
using Pro.Web.Api.Library.Constants.Enums;
using Pro.Web.Api.Library.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pro.Web.Api.Controllers
{
    [ApiController]
    [Route("armls/[controller]")]
    public class SsoController : ControllerBase
    {
        private readonly ISsoService _ssoService;
        private readonly IPartnerService _partnerService;
        private readonly ISamlServiceProvider _samlServiceProvider;
        private readonly IConfiguration _configuration;
        public SsoController(ISsoService ssoService, IPartnerService partnerService, ISamlServiceProvider samlServiceProvider, IConfiguration configuration)
        {
            _ssoService = ssoService;
            _partnerService = partnerService;
            _samlServiceProvider = samlServiceProvider;
            _configuration = configuration;
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

            try
            {
                var ssoResult = await _samlServiceProvider.ReceiveSsoAsync();
                Console.WriteLine(ssoResult);

                // Extract user information from SAML attributes
                var user = ExtractUserProfileFromAttributes(ssoResult.Attributes);

                // Generate JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        // Add other claims as needed
                    }),
                    Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                // Send the JWT in the API response
                return Ok(new { Token = jwt, FirstName = user.FirstName, LastName = user.LastName, /* Add other user information */ });
            }
            catch (SamlProtocolException ex)
            {
                // Log or inspect the exception details here.
                // ex.Message, ex.InnerException, etc.
                Console.WriteLine(ex.Message);
            }

            return new EmptyResult();
        }

        private User ExtractUserProfileFromAttributes(IEnumerable<SamlAttribute> attributes)
        {
            var userProfile = new User();

            userProfile.FirstName = GetSamlAttributeValueByName(attributes, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
            userProfile.LastName = GetSamlAttributeValueByName(attributes, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname");
            userProfile.Email = GetSamlAttributeValueByName(attributes, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            // Add other attributes as needed

            return userProfile;
        }

        private string GetSamlAttributeValueByName(IEnumerable<SamlAttribute> attributes, string attributeName)
        {
            var attribute = attributes.FirstOrDefault(x => x.Name == attributeName);

            //// Check if the attribute exists and has values
            if (attribute != null && attribute.AttributeValues != null && attribute.AttributeValues.Any())
            {
                return attribute.ToString();
            }

            return string.Empty;
        }







    }

}

