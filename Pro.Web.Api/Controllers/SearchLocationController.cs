using Microsoft.AspNetCore.Mvc;
using Pro.Api.Model.Concrete;
using Pro.Api.Service.Services.Abstract;

namespace Pro.Web.Api.Controllers
{
    public class SearchLocationController : Controller
    {
        private readonly ITypeaheadService _typeaheadService;

        public SearchLocationController(ITypeaheadService typeaheadService)
        {
            _typeaheadService = typeaheadService;
        }

        [HttpGet]
        [Route("GetLocations")]
        public IActionResult GetLocations(int partnerId,string searchText)
        {
            var locations = _typeaheadService.GetTypeAheadOptions(searchText, partnerId);
            return Ok(locations);
        }
    }
}
