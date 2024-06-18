using BE.DATN.BL.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface ISubjectDL : IBaseDL<subject>
    {
        Task<(List<subject_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);

        Task<subject?> GetSubjectByClassRegistration(Guid id);
    }
}
