using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ScoreBL : BaseBL<score>, IScoreBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IScoreDL _scoreDL; 

        public ScoreBL(IScoreDL scoreDL, IUnitOfWork unitOfWork) : base(scoreDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _scoreDL = scoreDL;
        } 

        protected override void ValidateBusiness(score entity, ModelStatte state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<score> entities, ModelStatte statte)
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
