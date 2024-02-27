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
        Task<(List<student_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);

        Task<List<student>?> GetByListCodeAsync(List<string> studentCodes);
    }
}
