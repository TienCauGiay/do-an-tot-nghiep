using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Score;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IScoreBL : IBaseBL<score>
    {
        Task<ResponseServiceScore> GetFilterPagingAsync(int limit, int offset, string? textSearch);
        Task<ReponseService> GetByStudentIdScoreViewAsync(Guid student_id); 
        Task<ReponseService> ImportExcelAsync(IFormFile formFile);
    }
}
