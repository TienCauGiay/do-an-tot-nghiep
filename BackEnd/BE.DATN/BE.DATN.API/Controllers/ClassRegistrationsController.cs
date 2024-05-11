using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.ClassRegistration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRegistrationsController : BaseController<class_registration>
    {
        private readonly IClassRegistrationBL _classRegistrationBL;

        public ClassRegistrationsController(IClassRegistrationBL classRegistrationBL) : base(classRegistrationBL)
        {
            _classRegistrationBL = classRegistrationBL;
        }
    }
}
