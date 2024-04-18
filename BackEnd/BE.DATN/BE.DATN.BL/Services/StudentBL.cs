using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Core;
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

        private readonly IClassesDL _classesDL;

        private readonly IClassRegistrationDL _classRegistrationDL;

        public StudentBL(IStudentDL studentDL, IClassesDL classesDL, IClassRegistrationDL classRegistrationDL, IUnitOfWork unitOfWork) : base(studentDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentDL = studentDL;
            _classesDL = classesDL;
            _classRegistrationDL = classRegistrationDL;
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

        public async Task<ResponseServiceStudent> GetFilterPagingAsync(int limit, int offset, string? textSearch, string? customFilter)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }
                if (customFilter == null)
                {
                    customFilter = "1 = 1";
                }
                var res = await _studentDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
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

        public async Task<ResponseService> GetStatisticNumberStudentAsync()
        {
            try
            {
                var res = await _studentDL.GetStatisticNumberStudentAsync();
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseService> GetOptionFilter(EnumOptionFilterStudent optionFilter)
        {
            try
            {
                var res = new List<condition_data>();
                switch (optionFilter)
                {
                    case EnumOptionFilterStudent.Class:
                        var lstClass = await _classesDL.GetAllAsync(); 
                        if(lstClass != null && lstClass.Count > 0) 
                        {
                            lstClass = lstClass.Distinct().ToList();
                            foreach (var c in lstClass)
                            {
                                var conditionData = new condition_data()
                                {
                                    condition_code = c.classes_code,
                                    condition_name = c.classes_name
                                };
                                res.Add(conditionData);
                            }
                        }
                        break; 
                    default:
                        break;
                }
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
