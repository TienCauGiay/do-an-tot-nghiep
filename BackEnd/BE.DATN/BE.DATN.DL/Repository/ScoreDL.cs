using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Score; 
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
    public class ScoreDL : BaseDL<score>, IScoreDL
    {
        public ScoreDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }  

        public async Task<List<score_view>?> GetByStudentIdScoreViewAsync(Guid student_id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_student_id", student_id);
            var result = await _unitOfWork.Connection.QueryAsync<score_view>(
                "select * from public.func_get_by_student_id_score_view(:p_student_id)", 
                parameters, 
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction);
            return (List<score_view>?)result;
        }

        public async Task<(List<score_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var students = new List<score_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_score(:p_limit, :p_offset, :p_text_search); select count(score_id) from score",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                students = (await multiResult.ReadAsync<score_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (students, totalRecord);
        }
    }
}
