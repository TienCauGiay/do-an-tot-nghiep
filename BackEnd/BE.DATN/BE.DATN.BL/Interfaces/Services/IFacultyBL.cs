using BE.DATN.BL.Models.Faculty;
using BE.DATN.BL.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IFacultyBL : IBaseBL<faculty>
    {
        Task<ResponseServiceFaculty> GetFilterPagingAsync(int limit, int offset, string? textSearch);
    }
}
