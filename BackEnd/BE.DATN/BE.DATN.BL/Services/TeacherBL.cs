using BE.DATN.BL.Common;
using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Classes;
using BE.DATN.BL.Models.Faculty;
using BE.DATN.BL.Models.Response; 
using BE.DATN.BL.Models.Teacher;
using BE.DATN.BL.Models.User;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class TeacherBL : BaseBL<teacher>, ITeacherBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ITeacherDL _teacherDL;

        private readonly IFacultyDL _facultyDL;

        private readonly IRoleDL _roleDL;

        private readonly IUserDL _userDL;

        public TeacherBL(
            ITeacherDL teacherDL,
            IFacultyDL facultyDL,
            IRoleDL roleDL,
            IUserDL userDL,
            IUnitOfWork unitOfWork
            ) : base(teacherDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _teacherDL = teacherDL;
            _facultyDL = facultyDL; 
            _roleDL = roleDL;
            _userDL = userDL;
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

        public async Task<ResponseServiceTeacher> GetFilterPagingAsync(int limit, int offset, string? textSearch, string? customFilter)
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
                var res = await _teacherDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
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

        public async Task<ResponseService> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch)
        {
            try
            {
                if (string.IsNullOrEmpty(textSearch))
                {
                    textSearch = string.Empty;
                }
                var res = await _teacherDL.GetOptionFilterAsync(optionFilter, textSearch);
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        protected override async Task AfterInsertAsync(teacher entity)
        {
            // Lấy quyền
            var roles = await _roleDL.GetAllAsync();
            var role = roles.FirstOrDefault(x => x.role_code == EnumPermission.Teacher);

            // Tạo tài khoản đăng nhập
            var userNew = new user()
            {
                user_id = Guid.NewGuid(),
                user_name = entity.teacher_code,
                pass_word = "1",
                role_id = role.role_id,
                status = 1,
                created_by = "Bùi Ngọc Tiến",
                created_date = DateTime.Now,
                modified_by = "",
                modified_date = DateTime.Now,
            };

            await _userDL.InsertAsync(userNew);
        }

        protected override async Task AfterInsertMultipleAsync(List<teacher> entities)
        {
            if (entities != null && entities.Count > 0)
            {
                // Lấy quyền
                var roles = await _roleDL.GetAllAsync();
                var role = roles.FirstOrDefault(x => x.role_code == EnumPermission.Teacher);

                var listUserNew = new List<user>();
                foreach (var s in entities)
                {
                    var userNew = new user()
                    {
                        user_id = Guid.NewGuid(),
                        user_name = s.teacher_code,
                        pass_word = "1",
                        role_id = role.role_id,
                        status = 1,
                        created_by = "Bùi Ngọc Tiến",
                        created_date = DateTime.Now,
                        modified_by = "",
                        modified_date = DateTime.Now,
                    };
                    listUserNew.Add(userNew);
                }

                await _userDL.InsertMultipleAsync(listUserNew);
            }
        }

        public async Task<ResponseService> ImportExcelAsync(IFormFile formFile)
        {
            try
            {
                if (formFile == null || formFile.Length == 0)
                {
                    return new ResponseService
                    (
                        StatusCodes.Status400BadRequest,
                        "Không có file được upload",
                        new Object()
                    );
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Sử dụng EPPlus để đọc tệp Excel
                using (var package = new ExcelPackage(formFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Lấy trang tính đầu tiên

                    var listTeacherImport = new List<teacher_import>();
                    var teacherSave = new List<teacher>();
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DateTime dateTimeBirthday;
                        var birthdayConVert = worksheet.Cells[row, 2].Value.ToString();
                        DateTime.TryParseExact(birthdayConVert, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeBirthday);
                        var teacherRow = new teacher_import()
                        {
                            teacher_name = worksheet.Cells[row, 1].Value.ToString(),
                            birthday = dateTimeBirthday,
                            gender = worksheet.Cells[row, 3].Value.ToString(),
                            address = worksheet.Cells[row, 4].Value.ToString(),
                            phone_number = worksheet.Cells[row, 5].Value.ToString(),
                            email = worksheet.Cells[row, 6].Value.ToString(),
                            faculty_code = worksheet.Cells[row, 7].Value.ToString(),
                        };

                        listTeacherImport.Add(teacherRow);
                    }
                    if (listTeacherImport != null && listTeacherImport.Count > 0)
                    {
                        var teacherImportFirst = listTeacherImport[0];
                        // Lấy classes_id theo classes_code
                        var facultyByCode = new faculty();
                        if (teacherImportFirst != null)
                        {
                            facultyByCode = await _facultyDL.GetByCodeAsync(teacherImportFirst.faculty_code);
                        }

                        if (facultyByCode == null)
                        {
                            return new ResponseService
                            (
                                StatusCodes.Status400BadRequest,
                                "Mã khoa không tồn tại trong hệ thống",
                                new Object()
                            );
                        }

                        foreach (var t in listTeacherImport)
                        {
                            teacher studentItemSave = new teacher()
                            {
                                teacher_id = Guid.NewGuid(),
                                faculty_id = facultyByCode.faculty_id,
                                teacher_code = BNTUtil.GenerateCode(t.teacher_name, t.birthday),
                                teacher_name = t.teacher_name,
                                birthday = t.birthday,
                                gender = t.gender,
                                address = t.address,
                                phone_number = t.phone_number,
                                email = t.email,
                                created_by = "Bùi Ngọc Tiến",
                                created_date = DateTime.Now,
                                modified_by = "",
                                modified_date = DateTime.Now,
                            };
                            teacherSave.Add(studentItemSave);
                        }
                    }
                    // cất dữ liệu
                    var res = await InsertMultipleAsync(teacherSave);
                    return new ResponseService(StatusCodes.Status200OK, "Nhập khẩu giảng viên thành công", res);
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<MemoryStream> ExportExcelAsync(int limit, int offset, string? textSearch, string? customFilter)
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
                var listStudentExport = await _teacherDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);

                var res = await _teacherDL.ExportExcelAsync(listStudentExport.Item1);
                return res;
            }
            catch (Exception ex)
            {
                return new MemoryStream();
            }
        }
    }
}
