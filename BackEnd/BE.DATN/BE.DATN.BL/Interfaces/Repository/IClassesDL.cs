using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.Classes;
using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IClassesDL : IBaseDL<classes>
    {
        Task<(List<classes_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);
    }
}
