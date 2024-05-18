using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ClassRegistrationBL : BaseBL<class_registration>, IClassRegistrationBL
    {
        private readonly IClassRegistrationDL _classRegistrationDL;
        public ClassRegistrationBL(IClassRegistrationDL classRegistrationDL, IUnitOfWork unitOfWork) : base(classRegistrationDL, unitOfWork)
        {
            _classRegistrationDL = classRegistrationDL;
        }

        public async Task<ResponseServiceClassRegistration> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }

                List<class_registration_view>? data = null;
                int? totalRecord = null;

                var res = await _classRegistrationDL.GetFilterPagingAsync(limit, offset, textSearch);
                data = res.Item1;
                totalRecord = res.Item2;

                return new ResponseServiceClassRegistration()
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
                return new ResponseServiceClassRegistration()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<class_registration_view>()
                };
            }
        }

        public async Task<List<class_registration_view>?> GetMultipleByCodeAsync(string code)
        {
            try
            {
                var res = await _classRegistrationDL.GetMultipleByCodeAsync(code);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected override async Task AfterInsertAsync(class_registration entity)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(class_registration entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<class_registration> entities, ModelState statte)
        {
            
        }
    }
}
