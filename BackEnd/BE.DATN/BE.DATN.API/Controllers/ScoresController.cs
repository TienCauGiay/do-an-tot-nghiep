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
    }
}
