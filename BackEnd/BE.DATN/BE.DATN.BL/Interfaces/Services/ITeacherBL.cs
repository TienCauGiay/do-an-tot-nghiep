using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface ITeacherBL : IBaseBL<teacher>
    {
        Task<ResponseServiceTeacher> GetFilterPagingAsync(int limit, int offset, string? textSearch); 
    }
}
