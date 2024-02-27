using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Score;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetFilterPaging(int limit, int offset, string? textSearch)
        {
            var res = await _scoreBL.GetFilterPagingAsync(limit, offset, textSearch);
            return Ok(res);
        }

        [HttpGet("get_by_student_id_score_view")]
        public async Task<IActionResult> GetByStudentIdScoreView(Guid student_id)
        {
            var res = await _scoreBL.GetByStudentIdScoreViewAsync(student_id);
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
    }
}
