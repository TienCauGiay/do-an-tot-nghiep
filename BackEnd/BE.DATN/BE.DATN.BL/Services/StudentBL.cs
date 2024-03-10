using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Student;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class StudentBL : BaseBL<student>, IStudentBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IStudentDL _studentDL;

        public StudentBL(IStudentDL studentDL, IUnitOfWork unitOfWork) : base(studentDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentDL = studentDL;
        }

        protected override void ValidateBusiness(student entity, ModelState state)
        {

        }

        protected override void ValidateBusinessMultiple(List<student> entities, ModelState statte)
        {

        }

        protected override void ValidateBeforeDelete(Guid id)
        {

        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {

        }

        public async Task<ResponseServiceStudent> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }
                var res = await _studentDL.GetFilterPagingAsync(limit, offset, textSearch);
                return new ResponseServiceStudent()
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công",
                    Data = res.Item1,
                    TotalPage = (int)Math.Ceiling((decimal) (res.Item2 > 0 ? res.Item2 : 0) / limit),
                    TotalRecord = res.Item2,
                    CurrentPage = offset,
                    CurrentPageRecords = res.Item1?.Count()
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceStudent()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<student_view>()
                };
            }
        }

        public async Task<ReponseService> GetStatisticNumberStudentAsync()
        {
            try
            {
                var res = await _studentDL.GetStatisticNumberStudentAsync();
                return new ReponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch(Exception ex)
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
