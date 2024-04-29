using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response; 
using BE.DATN.BL.Models.User;
using Microsoft.AspNetCore.Http;
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

        public UserBL(IUserDL userDL, IUnitOfWork unitOfWork) : base(userDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userDL = userDL;
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

        protected override void ValidateBusinessMultiple(List<user> entities, ModelState statte)
        {
            
        }

        public async Task<ResponseServiceUser> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }
                var res = await _userDL.GetFilterPagingAsync(limit, offset, textSearch);
                return new ResponseServiceUser()
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công",
                    Data = res.Item1,
                    TotalPage = (int)Math.Ceiling((decimal)(res.Item2 > 0 ? res.Item2 : 0) / limit),
                    TotalRecord = res.Item2,
                    CurrentPage = offset,
                    CurrentPageRecords = res.Item1?.Count()
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
            var userById = await _userDL.GetByIdAsync(userUpdate.user_id);
            if(userById != null)
            {
                userUpdate.pass_word = userById.pass_word;
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
    }
}
