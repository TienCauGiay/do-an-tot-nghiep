using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IClassRegistrationDL : IBaseDL<class_registration>
    {
        Task<(List<class_registration_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);
        Task<List<class_registration_view>?> GetMultipleByCodeAsync(string code);
        Task<bool> CheckExistsInClassRegistraion(Guid class_registration_id, Guid teacher_id, Guid student_id);
        Task<List<student>?> CheckExistsInClassRegistraionMultiple(Guid classRegistrationId, Guid teacherId, string studentIds);
    }
}
