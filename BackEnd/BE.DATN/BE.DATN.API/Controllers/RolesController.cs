using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<role>
    {
        private readonly IRoleBL _roleBL;
        public RolesController(IRoleBL roleBL) : base(roleBL)
        {
            _roleBL = roleBL;
        }
    }
}
