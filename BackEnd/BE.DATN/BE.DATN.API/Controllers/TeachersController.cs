using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : BaseController<teacher>
    {
        private readonly ITeacherBL _teacherBL;

        public TeachersController(ITeacherBL teacherBL) : base(teacherBL)
        {
            _teacherBL = teacherBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch, string? customFilter)
        {
            var res = await _teacherBL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
            return Ok(res);
        }

        [HttpGet("get_option_filter")]
        public async Task<IActionResult> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch)
        {
            var res = await _teacherBL.GetOptionFilterAsync(optionFilter, textSearch);
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
            var res = await _teacherBL.ImportExcelAsync(file);
            return Ok(res);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(int limit, int offset, string? textSearch, string? customFilter)
        {
            MemoryStream memoryStream = await _teacherBL.ExportExcelAsync(limit, offset, textSearch, customFilter);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Danh_Sach_Giang_Vien",
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_Sach_Giang_Vien");
        }
    }
}
