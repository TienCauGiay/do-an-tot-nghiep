using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BE.DATN.DL.Repository
{
    public class ClassRegistrationDL : BaseDL<class_registration>, IClassRegistrationDL
    {
        public ClassRegistrationDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<class_registration_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var res = new List<class_registration_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_class_registration(:p_limit, :p_offset, :p_text_search); " +
                "select count(u.class_registration_id) from public.class_registration u where u.class_registration_code ilike '%' || :p_text_search || '%' or u.class_registration_name ilike '%' || :p_text_search || '%';",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                res = (await multiResult.ReadAsync<class_registration_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (res, totalRecord);
        }

        public async Task<List<class_registration_view>?> GetMultipleByCodeAsync(string code)
        {
            var selectQuery = $"select cr.*, t.teacher_name, s.subject_name, s2.student_code, s2.student_name FROM public.class_registration cr inner join teacher t on cr.teacher_id = t.teacher_id inner join subject s on cr.subject_id = s.subject_id inner join student s2 on cr.student_id = s2.student_id WHERE cr.class_registration_code = @Code";

            var parameters = new { Code = code };

            var result = await _unitOfWork.Connection.QueryAsync<class_registration_view>(selectQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return (List<class_registration_view>?)result;
        }
    }
}
