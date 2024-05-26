using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IStudentDL : IBaseDL<student>
    {
        Task<(List<student_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter);
        Task<List<student>?> GetByListCodeAsync(List<string> studentCodes);
        Task<List<statistic_number_student_view>?> GetStatisticNumberStudentAsync();
        Task<List<condition_data>?> GetOptionFilterAsync(EnumOptionFilter optionFilter, string textSearch);
        Task<MemoryStream> ExportExcelAsync(List<student_view>? listStudentExport); 
    }
}
