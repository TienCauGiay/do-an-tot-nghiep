using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class SubjectBL : BaseBL<subject>, ISubjectBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISubjectDL _subjectDL;

        public SubjectBL(ISubjectDL subjectDL, IUnitOfWork unitOfWork) : base(subjectDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subjectDL = subjectDL;
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(subject entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<subject> entities, ModelState statte)
        {
            
        }
    }
}
