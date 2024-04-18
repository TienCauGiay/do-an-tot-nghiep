﻿using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Student;
using Dapper;
using System.Data;

namespace BE.DATN.DL.Repository
{
    public class StudentDL : BaseDL<student>, IStudentDL
    {
        public StudentDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<student_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);
            parameters.Add("p_custom_filter", customFilter);

            var students = new List<student_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_student(:p_limit, :p_offset, :p_text_search, :p_custom_filter); " +
                "select count(student_id) from student st left join classes cl on st.classes_id = cl.classes_id where (st.student_code ilike '%' || :p_text_search || '%' or st.student_name ilike '%' || :p_text_search || '%') and (" + customFilter + ");", 
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                students = (await multiResult.ReadAsync<student_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (students, totalRecord);
        }


        public async Task<List<student>?> GetByListCodeAsync(List<string> studentCodes)
        {
            var query = "SELECT * FROM student WHERE student_code = ANY(@p_student_codes)";

            var students = await _unitOfWork.Connection.QueryAsync<student>(query, new { p_student_codes = studentCodes.ToArray() }, transaction: _unitOfWork.Transaction);

            return students.ToList();
        }

        public async Task<List<statistic_number_student_view>?> GetStatisticNumberStudentAsync()
        {
            var query = "SELECT * FROM func_get_statistic_number_student()";

            var res = await _unitOfWork.Connection.QueryAsync<statistic_number_student_view>(query, null, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return res.ToList();
        }
    }
}
