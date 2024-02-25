using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
