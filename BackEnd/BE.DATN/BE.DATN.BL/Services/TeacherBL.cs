using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response; 
using BE.DATN.BL.Models.Teacher;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class TeacherBL : BaseBL<teacher>, ITeacherBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ITeacherDL _teacherDL;

        public TeacherBL(ITeacherDL teacherDL, IUnitOfWork unitOfWork) : base(teacherDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _teacherDL = teacherDL;
        }

        protected override void ValidateBusiness(teacher entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<teacher> entities, ModelState statte)
        {
            
        }
        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        public async Task<ResponseServiceTeacher> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }
                var res = await _teacherDL.GetFilterPagingAsync(limit, offset, textSearch);
                return new ResponseServiceTeacher()
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
                return new ResponseServiceTeacher()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<teacher_view>()
                };
            }
        }
    }
}
