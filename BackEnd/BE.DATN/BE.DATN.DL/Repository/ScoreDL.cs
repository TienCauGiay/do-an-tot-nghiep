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

        public async Task<List<score_view>?> GetAllScoreViewAsync()
        {
            var result = await _unitOfWork.Connection.QueryAsync<score_view>(
                "SELECT * FROM public.func_get_all_score_view()",
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction
            );
            return result.ToList();
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
    }
}
