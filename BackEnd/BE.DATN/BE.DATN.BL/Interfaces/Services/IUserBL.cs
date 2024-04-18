using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IUserBL : IBaseBL<user>
    {
        Task<ResponseServiceUser> GetFilterPagingAsync(int limit, int offset, string? textSearch);
        Task<ResponseService> ResetPassWorkAsync(Guid user_id);
    }
}
