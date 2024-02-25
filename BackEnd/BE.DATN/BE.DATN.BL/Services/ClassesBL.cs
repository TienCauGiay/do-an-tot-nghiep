using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ClassesBL : BaseBL<classes>, IClassesBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IClassesDL _classesDL;

        public ClassesBL(IClassesDL classesDL, IUnitOfWork unitOfWork) : base(classesDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _classesDL = classesDL; 
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(classes entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<classes> entities, ModelState statte)
        {
            
        }
    }
}
