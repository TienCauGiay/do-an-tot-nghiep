using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Subject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : BaseController<subject>
    {
        private readonly ISubjectBL _subjectBL;

        public SubjectsController(ISubjectBL subjectBL) : base(subjectBL)
        {
            _subjectBL = subjectBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch)
        {
            var res = await _subjectBL.GetFilterPagingAsync(limit, offset, textSearch);
            return Ok(res);
        }
    }
}
