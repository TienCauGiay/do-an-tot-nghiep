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
        Task<(List<teacher_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);
    }
}
