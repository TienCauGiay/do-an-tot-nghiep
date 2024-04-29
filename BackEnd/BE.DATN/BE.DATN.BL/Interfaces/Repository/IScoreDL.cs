using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Score; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IScoreDL : IBaseDL<score>
    {
        Task<(List<score_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter); 
        Task<List<score_view>?> GetByStudentIdScoreViewAsync(Guid student_id);
        Task<List<condition_data>?> GetOptionFilterAsync(EnumOptionFilter optionFilter, string textSearch);
        Task<MemoryStream> ExportExcelAsync(List<score_view>? listScoreExport);
    }
}
