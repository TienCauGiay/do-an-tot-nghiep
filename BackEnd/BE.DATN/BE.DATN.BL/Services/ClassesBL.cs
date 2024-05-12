using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Classes;
using BE.DATN.BL.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ClassesBL : BaseBL<classes>, IClassesBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IClassesDL _classesDL;

        public ClassesBL(IClassesDL classesDL, IUnitOfWork unitOfWork) : base(classesDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _classesDL = classesDL; 
        }

        public async Task<ResponseServiceClasses> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }

                List<classes_view>? data = null;
                int? totalRecord = null;

                var res = await _classesDL.GetFilterPagingAsync(limit, offset, textSearch);
                data = res.Item1;
                totalRecord = res.Item2;

                return new ResponseServiceClasses()
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
                return new ResponseServiceClasses()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<classes_view>()
                };
            }
        }

        protected override async Task AfterInsertAsync(classes entity)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(classes entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<classes> entities, ModelState statte)
        {
            
        }
    }
}
