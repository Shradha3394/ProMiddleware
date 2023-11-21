using Microsoft.AspNetCore.Mvc;
using Pro.Api.Model.Concrete;
using Pro.Api.Service.Services.Abstract;

namespace Pro.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpGet]
        [Route("GetPartner")]
        public IActionResult GetPartner(string? privateSiteLabel)
        {
            var partners = _partnerService.GetPartnersTable();
            if (string.IsNullOrEmpty(privateSiteLabel))
                return Ok(partners.FirstOrDefault());
            var partner = partners.FirstOrDefault(p => p.PrivateLabelSite!.Equals(privateSiteLabel,StringComparison.CurrentCultureIgnoreCase));
            if (partner == null)
            {
                return Unauthorized();
            }

            return Ok(partner);
        }




    }
}