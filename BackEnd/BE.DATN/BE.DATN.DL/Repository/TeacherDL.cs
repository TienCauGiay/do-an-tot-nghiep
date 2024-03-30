using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork; 
using BE.DATN.BL.Models.Teacher;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class TeacherDL : BaseDL<teacher>, ITeacherDL
    {
        public TeacherDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<teacher_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var teachers = new List<teacher_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_teacher(:p_limit, :p_offset, :p_text_search); " +
                "select count(tc.teacher_id) from teacher tc left join faculty f on tc.faculty_id = f.faculty_id where tc.teacher_code ilike '%' || :p_text_search || '%' or tc.teacher_name ilike '%' || :p_text_search || '%';",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                teachers = (await multiResult.ReadAsync<teacher_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (teachers, totalRecord);
        }
    }
}
