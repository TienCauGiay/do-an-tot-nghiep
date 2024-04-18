using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IStudentBL : IBaseBL<student>
    {
        Task<ResponseServiceStudent> GetFilterPagingAsync(int limit, int offset, string? textSearch, string? customFilter);
        Task<ResponseService> GetStatisticNumberStudentAsync();

        Task<ResponseService> GetOptionFilter(EnumOptionFilterStudent optionFilter);
    }
}
