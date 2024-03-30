using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Faculty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultysController : BaseController<faculty>
    {
        private readonly IFacultyBL _facultyBL;

        public FacultysController(IFacultyBL facultyBL) : base(facultyBL)
        {
            _facultyBL = facultyBL;
        }
    }
}
