using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class RoleBL : BaseBL<role>, IRoleBL
    {
        private readonly IRoleDL _roleDL;

        public RoleBL(IRoleDL roleDL, IUnitOfWork unitOfWork) : base(roleDL, unitOfWork)
        {
            _roleDL = roleDL;
        }

        protected override async Task AfterInsertAsync(role entity)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(role entity, ModelState state)
        {
            
        }
    }
}
