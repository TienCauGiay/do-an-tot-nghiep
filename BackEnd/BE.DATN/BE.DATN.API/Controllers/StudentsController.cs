using BE.DATN.BL.Enums;
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
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch, string? customFilter)
        {
            var res = await _studentBL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
            return Ok(res);
        }

        [HttpGet("get_statistic_number_student")]
        public async Task<IActionResult> GetStatisticNumberStudent()
        {
            var res = await _studentBL.GetStatisticNumberStudentAsync();
            return Ok(res);
        }

        [HttpGet("get_option_filter")]
        public async Task<IActionResult> GetOptionFilter(EnumOptionFilterStudent option_filter)
        {
            var res = await _studentBL.GetOptionFilter(option_filter);
            return Ok(res);
        }
    }
}
