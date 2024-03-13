using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<user>
    {
        private readonly IUserBL _userBL;
        public UsersController(IUserBL userBL) : base(userBL)
        {
            _userBL = userBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch)
        {
            var res = await _userBL.GetFilterPagingAsync(limit, offset, textSearch);
            return Ok(res);
        }

        [HttpPut("reset_pass_word")]
        public async Task<IActionResult> ResetPassWork(Guid user_id)
        {
            var res = await _userBL.ResetPassWorkAsync(user_id);
            return Ok(res); 
        }
    }
}
