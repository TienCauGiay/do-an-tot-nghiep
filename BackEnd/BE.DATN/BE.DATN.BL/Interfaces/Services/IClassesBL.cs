using BE.DATN.BL.Models.Classes;
using BE.DATN.BL.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IClassesBL : IBaseBL<classes>
    {
        Task<ResponseServiceClasses> GetFilterPagingAsync(int limit, int offset, string? textSearch);
    }
}
