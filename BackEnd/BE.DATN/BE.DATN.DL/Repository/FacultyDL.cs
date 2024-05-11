using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Faculty;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class FacultyDL : BaseDL<faculty>, IFacultyDL
    {
        public FacultyDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<faculty_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var res = new List<faculty_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_faculty(:p_limit, :p_offset, :p_text_search); " +
                "select count(u.faculty_id) from public.faculty u where u.faculty_code ilike '%' || :p_text_search || '%' or u.faculty_name ilike '%' || :p_text_search || '%';",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                res = (await multiResult.ReadAsync<faculty_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (res, totalRecord);
        }
    }
}
