using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IClassRegistrationBL : IBaseBL<class_registration>
    {
        Task<ResponseServiceClassRegistration> GetFilterPagingAsync(int limit, int offset, string? textSearch);

        Task<List<class_registration_view>?> GetMultipleByCodeAsync(string code);
    }
}
