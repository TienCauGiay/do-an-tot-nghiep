using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Subject;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class SubjectBL : BaseBL<subject>, ISubjectBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISubjectDL _subjectDL;

        public SubjectBL(ISubjectDL subjectDL, IUnitOfWork unitOfWork) : base(subjectDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subjectDL = subjectDL;
        }

        public async Task<ResponseServiceSubject> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }

                List<subject_view>? data = null;
                int? totalRecord = null;

                var res = await _subjectDL.GetFilterPagingAsync(limit, offset, textSearch);
                data = res.Item1;
                totalRecord = res.Item2;

                return new ResponseServiceSubject()
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
                return new ResponseServiceSubject()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<subject_view>()
                };
            }
        }

        protected override async Task AfterInsertAsync(subject entity)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(subject entity, ModelState state)
        {
            
        }
    }
}
