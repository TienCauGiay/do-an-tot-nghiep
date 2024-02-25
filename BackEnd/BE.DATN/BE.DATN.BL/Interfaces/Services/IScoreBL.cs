using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IScoreBL : IBaseBL<score>
    {
        Task<ReponseService> GetAllScoreViewAsync();
        Task<ReponseService> GetByStudentIdScoreViewAsync(Guid student_id);
    }
}
