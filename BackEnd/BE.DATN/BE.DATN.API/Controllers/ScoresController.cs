﻿using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Score;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : BaseController<score>
    {
        private readonly IScoreBL _scoreBL;

        public ScoresController(IScoreBL scoreBL) : base(scoreBL)
        {
            _scoreBL = scoreBL;
        }

        [HttpGet("paging_filter")]
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch, string? customFilter)
        {
            var res = await _scoreBL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
            return Ok(res);
        }

        [HttpGet("get_by_student_id_score_view")]
        public async Task<IActionResult> GetByStudentIdScoreView(Guid student_id)
        {
            var res = await _scoreBL.GetByStudentIdScoreViewAsync(student_id);
            return Ok(res);
        }

        [HttpGet("get_option_filter")]
        public async Task<IActionResult> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch)
        {
            var res = await _scoreBL.GetOptionFilterAsync(optionFilter, textSearch);
            return Ok(res);
        }

        [HttpPost("import_excel")]
        public async Task<IActionResult> ImportExcel()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.GetFile("file");
            var res = await _scoreBL.ImportExcelAsync(file);
            return Ok(res);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(int limit, int offset, string? textSearch, string? customFilter)
        {
            MemoryStream memoryStream = await _scoreBL.ExportExcelAsync(limit, offset, textSearch, customFilter);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Danh_Sach_Diem",
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_Sach_Diem");
        }
    }
}
