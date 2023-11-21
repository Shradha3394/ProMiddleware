using Microsoft.AspNetCore.Mvc;
using Pro.Api.Model.Concrete;
using Pro.Api.Service.Services.Abstract;

namespace Pro.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("SignIn")]
        public IActionResult SignIn(string logonName, string password, int partnerId)
        {
            var user = _userService.SignInByPassword(logonName, password, partnerId);
            if (user == null)
            {
                return Ok(new BaseResponse<User>(StatusCodes.Status404NotFound, "Email or password is incorrect"));
            }
            return Ok(new BaseResponse<User>(StatusCodes.Status200OK, "Email or password is incorrect", user));
        }
    }
}