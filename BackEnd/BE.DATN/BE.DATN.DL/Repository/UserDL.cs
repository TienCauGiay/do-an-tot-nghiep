using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
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

        public async Task<(List<user_view>?, int?)> GetFilterPagingByRoleAsync(int limit, int offset, string textSearch, Guid userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);
            parameters.Add("p_user_id", userId);

            var users = new List<user_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_by_role_user(:p_limit, :p_offset, :p_text_search, :p_user_id); " +
                "select count(u.user_id) from public.user u where u.user_name ilike '%' || :p_text_search || '%' and u.user_id = :p_user_id;",
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

        public async Task<login?> CheckLoginAsync(string user_name, string? pass_word)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_user_name", user_name);
            parameters.Add("p_pass_word", pass_word);

            string query = "select u.*, r.role_code from public.user u inner join public.role r on u.role_id = r.role_id where u.user_name = :p_user_name and u.pass_word = :p_pass_word;";

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<login>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
            return (login?)result;
        }

        public async Task DeleteByUserName(string userName)
        {
            var deleteQuery = $"DELETE FROM public.user WHERE user_name = @UserName";
            var parameters = new { UserName = userName };
            await _unitOfWork.Connection.ExecuteAsync(deleteQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task DeleteByListUserName(List<string> userNameList)
        {
            var query = $"DELETE FROM public.user WHERE user_name = ANY(@userNameList)";

            await _unitOfWork.Connection.ExecuteAsync(query, new { userNameList = userNameList.ToArray() }, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task<user?> GetByUsernameAsync(string username)
        {
            var selectQuery = $"SELECT * FROM public.user WHERE user_name = @Username";

            var parameters = new { Username = username };

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<user>(selectQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
