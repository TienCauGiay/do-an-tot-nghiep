using BE.DATN.BL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IUserDL : IBaseDL<user>
    {
        Task<(List<user_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);
        Task<int> ResetPassWorkAsync(Guid user_id);
        Task<login?> CheckLoginAsync(string user_name, string? pass_word);
    }
}
