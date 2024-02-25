using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class TeacherBL : BaseBL<teacher>, ITeacherBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ITeacherDL _teacherDL;

        public TeacherBL(ITeacherDL teacherDL, IUnitOfWork unitOfWork) : base(teacherDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _teacherDL = teacherDL;
        }

        protected override void ValidateBusiness(teacher entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<teacher> entities, ModelState statte)
        {
            
        }
        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        } 
    }
}
