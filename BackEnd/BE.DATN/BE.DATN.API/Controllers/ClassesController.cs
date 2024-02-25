using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : BaseController<classes>
    {
        private readonly IClassesBL _classesBL;

        public ClassesController(IClassesBL classesBL) : base(classesBL)
        {
            _classesBL = classesBL;
        }
    }
}
