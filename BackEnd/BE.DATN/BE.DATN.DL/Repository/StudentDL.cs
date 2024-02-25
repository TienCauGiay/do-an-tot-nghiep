using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
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

        public async Task<(List<student_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var students = new List<student_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_student(:p_limit, :p_offset, :p_text_search); select count(student_id) from student",
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
    }
}
