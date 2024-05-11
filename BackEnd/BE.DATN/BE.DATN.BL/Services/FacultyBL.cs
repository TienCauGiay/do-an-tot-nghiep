using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Faculty;
using BE.DATN.BL.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class FacultyBL : BaseBL<faculty>, IFacultyBL
    {
        private readonly IFacultyDL _facultyDL;
        public FacultyBL(IFacultyDL facultyDL, IUnitOfWork unitOfWork) : base(facultyDL, unitOfWork)
        {
            _facultyDL = facultyDL;
        }

        public async Task<ResponseServiceFaculty> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }

                List<faculty_view>? data = null;
                int? totalRecord = null;

                var res = await _facultyDL.GetFilterPagingAsync(limit, offset, textSearch);
                data = res.Item1;
                totalRecord = res.Item2;

                return new ResponseServiceFaculty()
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
                return new ResponseServiceFaculty()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<faculty_view>()
                };
            }
        }

        protected override async Task AfterInsertAsync(faculty entity)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(faculty entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<faculty> entities, ModelState statte)
        {
            
        }
    }
}
