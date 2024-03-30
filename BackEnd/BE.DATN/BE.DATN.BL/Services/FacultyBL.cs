using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class FacultyBL : BaseBL<faculty>, IFacultyBL
    {
        private readonly IFacultyDL _facultyDL;
        public FacultyBL(IFacultyDL facultyDL, IUnitOfWork unitOfWork) : base(facultyDL, unitOfWork)
        {
            _facultyDL = facultyDL;
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(faculty entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<faculty> entities, ModelState statte)
        {
            
        }
    }
}
