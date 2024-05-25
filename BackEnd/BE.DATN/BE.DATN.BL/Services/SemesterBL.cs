using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class SemesterBL : BaseBL<semester>, ISemesterBL
    {
        private readonly ISemesterDL _semesterDL;

        public SemesterBL(ISemesterDL semesterDL, IUnitOfWork unitOfWork) : base(semesterDL, unitOfWork)
        {
            _semesterDL = semesterDL;
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(semester entity, ModelState state)
        {
            
        }
    }
}
