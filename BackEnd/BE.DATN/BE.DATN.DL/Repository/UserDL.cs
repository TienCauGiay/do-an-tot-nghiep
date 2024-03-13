﻿using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork; 
using BE.DATN.BL.Models.User;
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
    public class UserDL : BaseDL<user>, IUserDL
    {  
        public UserDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<user_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);

            var users = new List<user_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_user(:p_limit, :p_offset, :p_text_search); " +
                "select count(u.user_id) from public.user u where u.user_name ilike '%' || :p_text_search || '%';",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                users = (await multiResult.ReadAsync<user_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (users, totalRecord);
        }

        public async Task<int> ResetPassWorkAsync(Guid user_id)
        {
            var updateQuery = $"update public.user set pass_word = 1 where user_id = @Id";

            var parameters = new { Id = user_id };

            var result = await _unitOfWork.Connection.ExecuteAsync(updateQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
