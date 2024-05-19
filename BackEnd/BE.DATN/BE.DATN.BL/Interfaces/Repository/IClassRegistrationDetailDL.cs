using BE.DATN.BL.Models.ClassRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IClassRegistrationDetailDL : IBaseDL<class_registration_detail>
    {
        Task <List<class_registration_detail>?> GetListByIdMasterAsync(Guid id);
        Task<int> DebeteByIdMaster(Guid id);
        Task<int> DebeteMultipleByListIdMaster(List<Guid> ids);
    }
}
