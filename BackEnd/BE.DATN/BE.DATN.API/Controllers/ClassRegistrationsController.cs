using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.ClassRegistration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRegistrationsController : BaseController<class_registration>
    {
        private readonly IClassRegistrationBL _classRegistrationBL;

        public ClassRegistrationsController(IClassRegistrationBL classRegistrationBL) : base(classRegistrationBL)
        {
            _classRegistrationBL = classRegistrationBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch)
        {
            var res = await _classRegistrationBL.GetFilterPagingAsync(limit, offset, textSearch);
            return Ok(res);
        }

        [HttpGet("get_multiple_by_code")]
        public async Task<IActionResult> GetMultipleByCode(string code)
        {
            var res = await _classRegistrationBL.GetMultipleByCodeAsync(code);
            return Ok(res);
        }

        [HttpPost("insert_master_detail")]
        public async Task<IActionResult> InsertMasterDetail(class_registration_entity model)
        {
            var res = await _classRegistrationBL.InsertMasterDetailAsync(model);
            return Ok(res);
        }

        [HttpPut("update_master_detail")]
        public async Task<IActionResult> UpdateMasterDetail(class_registration_entity model)
        {
            var res = await _classRegistrationBL.UpdateMasterDetailAsync(model);
            return Ok(res);
        }

        [HttpGet("get_list_detail")]
        public async Task<IActionResult> GetListDetail(Guid id)
        {
            var res = await _classRegistrationBL.GetListDetailAsync(id);
            return Ok(res);
        }
    }
}
