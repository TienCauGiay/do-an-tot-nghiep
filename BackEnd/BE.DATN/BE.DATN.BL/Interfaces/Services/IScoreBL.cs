using BE.DATN.BL.Enums;
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
        Task<ResponseServiceScore> GetFilterPagingAsync(int limit, int offset, string? textSearch, string? customFilter);
        Task<ResponseService> GetByStudentIdScoreViewAsync(Guid student_id);
        Task<ResponseService> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch);
        Task<ResponseService> ImportExcelAsync(IFormFile formFile);
        Task<MemoryStream> ExportExcelAsync(int limit, int offset, string? textSearch, string? customFilter);
        Task<ResponseService> CheckExistsStudentInClassRegitrationAsync(score entity);
    }
}
