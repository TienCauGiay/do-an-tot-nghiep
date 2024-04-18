using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBL _authBL;

        public AuthController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpPost("authen")]
        public async Task<IActionResult> Login([FromBody] user user)
        {
            var res = await _authBL.LoginAsync(user);
            return Ok(res);
        }
    }
}
