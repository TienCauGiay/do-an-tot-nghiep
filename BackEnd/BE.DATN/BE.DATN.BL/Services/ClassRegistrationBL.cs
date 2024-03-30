using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ClassRegistrationBL : BaseBL<class_registration>, IClassRegistrationBL
    {
        private readonly IClassRegistrationDL _classRegistrationDL;
        public ClassRegistrationBL(IClassRegistrationDL classRegistrationDL, IUnitOfWork unitOfWork) : base(classRegistrationDL, unitOfWork)
        {
            _classRegistrationDL = classRegistrationDL;
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(class_registration entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<class_registration> entities, ModelState statte)
        {
            
        }
    }
}
