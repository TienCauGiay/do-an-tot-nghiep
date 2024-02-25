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

        [HttpGet("get_all_score_view")]
        public async Task<IActionResult> GetAllScoreView()
        {
            var res = await _scoreBL.GetAllScoreViewAsync();  
            return Ok(res);
        }

        [HttpGet("get_by_student_id_score_view")]
        public async Task<IActionResult> GetByStudentIdScoreView(Guid student_id)
        {
            var res = await _scoreBL.GetByStudentIdScoreViewAsync(student_id);
            return Ok(res);
        }
    }
}
