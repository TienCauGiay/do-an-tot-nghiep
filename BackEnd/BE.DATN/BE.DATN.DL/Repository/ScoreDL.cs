using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class ScoreDL : BaseDL<score>, IScoreDL
    {
        public ScoreDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
