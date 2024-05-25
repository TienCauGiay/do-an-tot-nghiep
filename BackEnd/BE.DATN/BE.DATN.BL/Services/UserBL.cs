using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response; 
using BE.DATN.BL.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class UserBL : BaseBL<user>, IUserBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserDL _userDL;

        private readonly ITokenService _tokenService;

        private readonly IConfiguration _configuration;

        private Guid _userId = Guid.Empty;

        private string _roleCode = "";

        public UserBL(
                IUserDL userDL, 
                IUnitOfWork unitOfWork,
                ITokenService tokenService,
                IConfiguration configuration
            ) : base(userDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userDL = userDL;
            _tokenService = tokenService;
            _configuration = configuration;
        } 

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(user entity, ModelState state)
        {
            
        } 

        public async Task<ResponseServiceUser> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                var token = _tokenService.GetToken();
                var jwtService = new JwtService(_configuration["JwtSettings:SecretKey"], _configuration);

                // Giải mã token
                var principal = jwtService.ValidateToken(token);

                // Truy cập thông tin từ các claims
                if (principal != null)
                {
                    _userId = Guid.Parse(principal.FindFirst("user_id")?.Value);
                    _roleCode = principal.FindFirst("role_code")?.Value;
                }
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }

                List<user_view>? data = null;
                int? totalRecord = null;

                if (_roleCode == EnumPermission.Admin.ToString())
                {
                    var res = await _userDL.GetFilterPagingAsync(limit, offset, textSearch);
                    data = res.Item1;
                    totalRecord = res.Item2;
                }
                else
                {
                    var res = await _userDL.GetFilterPagingByRoleAsync(limit, offset, textSearch, _userId);
                    data = res.Item1;
                    totalRecord = res.Item2;
                }

                return new ResponseServiceUser()
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công",
                    Data = data,
                    TotalPage = (int)Math.Ceiling((decimal)(totalRecord > 0 ? totalRecord : 0) / limit),
                    TotalRecord = totalRecord,
                    CurrentPage = offset,
                    CurrentPageRecords = data?.Count()
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceUser()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<user_view>()
                };
            }
        }

        public override async Task<ResponseService> UpdateAsync(user userUpdate)
        {
            var token = _tokenService.GetToken();
            var jwtService = new JwtService(_configuration["JwtSettings:SecretKey"], _configuration);

            // Giải mã token
            var principal = jwtService.ValidateToken(token);

            // Truy cập thông tin từ các claims
            if (principal != null)
            {
                _roleCode = principal.FindFirst("role_code")?.Value;
            }
            if (_roleCode == EnumPermission.Admin.ToString())
            {
                var userById = await _userDL.GetByIdAsync(userUpdate.user_id);
                if (userById != null)
                {
                    userUpdate.pass_word = userById.pass_word;
                    return await base.UpdateAsync(userUpdate);
                }
            }
            else
            {
                return await base.UpdateAsync(userUpdate);
            }
            return new ResponseService
                (
                        StatusCodes.Status500InternalServerError,
                        "Cập nhật thông tin user không thành công",
                        new Object()
                );
        }

        public async Task<ResponseService> ResetPassWorkAsync(Guid user_id)
        {
            try
            { 
                var res = await _userDL.ResetPassWorkAsync(user_id);
                return new ResponseService(StatusCodes.Status200OK, "Lấy lại mật khẩu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService(StatusCodes.Status500InternalServerError, "Lấy lại mật khẩu không thành công", new Object());
            }
        }

        protected override async Task AfterInsertAsync(user entity)
        {
            
        }

        public async Task<ResponseService> GetByUsernameAsync(string username)
        {
            try
            {
                var res = await _userDL.GetByUsernameAsync(username);
                return new ResponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }
    }
}
