using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Subject;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class SubjectDL : BaseDL<subject>, ISubjectDL
    {
        public SubjectDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<subject_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var res = new List<subject_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_subject(:p_limit, :p_offset, :p_text_search); " +
                "select count(u.subject_id) from public.subject u where u.subject_code ilike '%' || :p_text_search || '%' or u.subject_name ilike '%' || :p_text_search || '%';",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                res = (await multiResult.ReadAsync<subject_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (res, totalRecord);
        }

        protected override string BuildQueryCheckArise()
        {
            var query = @"
                select 1 
                from class_registration cr 
                where cr.subject_id = @Id  
                limit 1";
            return query;
        }

        protected override string BuildQueryGetIdArise()
        {
            var query = "select * from public.function_get_subject_id_arise(:p_ids)";
            return query;
        }
    }
}
