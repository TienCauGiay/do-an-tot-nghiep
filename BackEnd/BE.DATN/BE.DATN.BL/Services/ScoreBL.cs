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
        private readonly IScoreDL _scoreDL;

        private readonly IUnitOfWork _unitOfWork;

        public ScoreBL(IScoreDL scoreDL, IUnitOfWork unitOfWork) : base(scoreDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _scoreDL = scoreDL;
        }
    }
}
