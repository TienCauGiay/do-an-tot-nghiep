using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        public async Task<IActionResult> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch)
        {
            var res = await _studentBL.GetOptionFilterAsync(optionFilter, textSearch);
            return Ok(res);
        }

        /// <summary>
        /// Nhập khẩu danh sách sinh viên
        /// </summary>
        /// <returns></returns>
        [HttpPost("import_excel")]
        public async Task<IActionResult> ImportExcel()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.GetFile("file");
            var res = await _studentBL.ImportExcelAsync(file);
            return Ok(res);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(int limit, int offset, string? textSearch, string? customFilter)
        {
            MemoryStream memoryStream = await _studentBL.ExportExcelAsync(limit, offset, textSearch, customFilter);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Danh_Sach_Sinh_Vien",
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_Sach_Sinh_Vien");
        }

        [HttpGet("check_arise")]
        public async Task<IActionResult> CheckArise(Guid student_id)
        {
            var res = await _studentBL.CheckAriseAsync(student_id);
            return Ok(res);
        }

        [HttpPost("get_id_arise")]
        public async Task<IActionResult> GetIdAriseMultiple([FromBody] List<Guid> ids)
        {
            var res = await _studentBL.GetIdAriseMultipleAsync(ids);
            return Ok(res);
        }
    }
}
