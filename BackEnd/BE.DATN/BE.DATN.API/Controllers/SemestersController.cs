using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Semester;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : BaseController<semester>
    {
        private readonly ISemesterBL _semesterBL;
        public SemestersController(ISemesterBL semesterBL) : base(semesterBL)
        {
            _semesterBL = _semesterBL;
        }
    }
}
