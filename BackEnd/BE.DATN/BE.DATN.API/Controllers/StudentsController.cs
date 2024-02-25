using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController<student>
    {
        private readonly IStudentBL _studentBL;

        public StudentsController(IStudentBL studentBL) : base(studentBL)
        {
            _studentBL = studentBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch)
        {
            var res = await _studentBL.GetFilterPagingAsync(limit, offset, textSearch);
            return Ok(res);
        }
    }
}
