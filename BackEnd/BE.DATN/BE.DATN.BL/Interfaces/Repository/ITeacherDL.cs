using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface ITeacherDL : IBaseDL<teacher>
    {
        Task<(List<teacher_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter);
        Task<List<condition_data>?> GetOptionFilterAsync(EnumOptionFilter optionFilter, string textSearch);
        Task<MemoryStream> ExportExcelAsync(List<teacher_view>? listTeacherExport);
        Task<bool> CheckAriseAsync(Guid teacher_id);
        Task<List<Guid>?> GetIdAriseMultipleAsync(List<Guid> teacherIds);
    }
}
