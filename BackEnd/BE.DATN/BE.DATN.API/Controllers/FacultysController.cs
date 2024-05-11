using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Faculty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultysController : BaseController<faculty>
    {
        private readonly IFacultyBL _facultyBL;

        public FacultysController(IFacultyBL facultyBL) : base(facultyBL)
        {
            _facultyBL = facultyBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch)
        {
            var res = await _facultyBL.GetFilterPagingAsync(limit, offset, textSearch);
            return Ok(res);
        }
    }
}
