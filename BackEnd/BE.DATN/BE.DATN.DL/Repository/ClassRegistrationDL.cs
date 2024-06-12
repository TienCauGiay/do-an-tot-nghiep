using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Student;
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
                "select count(u.class_registration_id) from public.class_registration u inner join subject s on u.subject_id = s.subject_id inner join teacher t on u.teacher_id = t.teacher_id where u.class_registration_code ilike '%' || :p_text_search || '%' or u.class_registration_name ilike '%' || :p_text_search || '%' or s.subject_name ilike '%' || :p_text_search || '%' or t.teacher_name ilike '%' || :p_text_search || '%';",
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

        protected override string BuildQueryCheckArise()
        {
            var query = @"
                select 1 
                from score s
                where s.class_registration_id = @Id  
                limit 1";
            return query;
        }

        protected override string BuildQueryGetIdArise()
        {
            var query = "select * from public.function_get_registration_id_arise(:p_ids)";
            return query;
        }

        public async Task<bool> CheckExistsInClassRegistraion(Guid class_registration_id, Guid teacher_id, Guid student_id)
        {
            var query = $"select cr.* from class_registration cr inner join class_registration_detail crd on cr.class_registration_id  = crd.class_registration_id where cr.class_registration_id = @ClassRegistrationId and cr.teacher_id = @TeacherId and crd.student_id = @StudentId;";

            var parameters = new
            {
                ClassRegistrationId = class_registration_id,
                TeacherId = teacher_id,
                StudentId = student_id
            };

            var result = await _unitOfWork.Connection.QueryAsync<class_registration>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            if (result != null && result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<student>?> CheckExistsInClassRegistraionMultiple(Guid classRegistrationId, Guid teacherId, string studentIds)
        {
            var query = "SELECT * FROM func_check_exists_in_class_registration(:p_class_registration_id, :p_teacher_id, :p_student_ids)";

            var parameters = new
            {
                p_class_registration_id = classRegistrationId,
                p_teacher_id = teacherId,
                p_student_ids = studentIds
            };

            var res = await _unitOfWork.Connection.QueryAsync<student>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return res.ToList();
        }

        public async Task<List<student>?> GetStudentHasScore(Guid classRegistrationId, Guid teacherId, string studentIds)
        {
            var query = "SELECT * FROM func_get_student_has_score(:p_class_registration_id, :p_teacher_id, :p_student_ids)";

            var parameters = new
            {
                p_class_registration_id = classRegistrationId,
                p_teacher_id = teacherId,
                p_student_ids = studentIds
            };

            var res = await _unitOfWork.Connection.QueryAsync<student>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return res.ToList();
        }
    }
}
