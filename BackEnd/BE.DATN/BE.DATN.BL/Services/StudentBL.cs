using BE.DATN.BL.Common;
using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Classes;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Score;
using BE.DATN.BL.Models.Student;
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
    public class StudentBL : BaseBL<student>, IStudentBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IStudentDL _studentDL;

        private readonly IClassesDL _classesDL;

        private readonly IClassRegistrationDL _classRegistrationDL;

        private readonly IRoleDL _roleDL;

        private readonly IUserDL _userDL;

        public StudentBL(IStudentDL studentDL, 
            IClassesDL classesDL, 
            IClassRegistrationDL classRegistrationDL, 
            IRoleDL roleDL,
            IUserDL userDL,
            IUnitOfWork unitOfWork) : base(studentDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentDL = studentDL;
            _classesDL = classesDL;
            _classRegistrationDL = classRegistrationDL;
            _roleDL = roleDL;
            _userDL = userDL;
        }

        protected override void ValidateBusiness(student entity, ModelState state)
        {

        }

        protected override async Task<Dictionary<string, object>?> ValidateBusinessMultiple(List<student> entities, ModelState statte)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            foreach (var s in entities)
            {
                if(s.birthday > DateTime.Now)
                {
                    res.Add("message_error", $"Ngày sinh của sinh viên {s.student_name} lớn hơn ngày hiện tại");
                    break;
                }
                if (BNTUtil.IsNotNumber(s.phone_number))
                {
                    res.Add("message_error", $"Điện thoại của sinh viên {s.student_name} phải là số");
                    break;
                }
                if (!BNTUtil.IsValidEmail(s.email))
                {
                    res.Add("message_error", $"Email của sinh viên {s.student_name} không đúng định dạng");
                    break;
                }
            }
            if(res.Count > 0)
            {
                return res;
            }
            else
            {
                return null;
            }
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

        public async Task<ResponseService> GetClassAverageScoreAsync()
        {
            try
            {
                var res = await _studentDL.GetClassAverageScoreAsync();
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseService> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch)
        {
            try
            {
                if(string.IsNullOrEmpty(textSearch))
                {
                    textSearch = string.Empty;  
                }   
                var res = await _studentDL.GetOptionFilterAsync(optionFilter, textSearch);
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService(StatusCodes.Status500InternalServerError, ex.Message);
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
                        new Dictionary<string, object>
                            {
                                { "message_error", "Không có file được upload" }
                            }
                    );
                }

                // Kiểm tra phần mở rộng của tệp có phải file excel hay không
                var fileExtension = Path.GetExtension(formFile.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    return new ResponseService
                    (
                        StatusCodes.Status400BadRequest,
                        "Tệp không phải là tệp Excel",
                        new Dictionary<string, object>
                            {
                        { "message_error", "Tệp không phải là tệp Excel" }
                            }
                    );
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Sử dụng EPPlus để đọc tệp Excel
                using (var package = new ExcelPackage(formFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Lấy trang tính đầu tiên

                    var listStudentImport = new List<student_import>();
                    var studentSave = new List<student>();
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DateTime dateTimeBirthday;
                        var birthdayConVert = worksheet.Cells[row, 2].Value.ToString();
                        DateTime.TryParseExact(birthdayConVert, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeBirthday);
                        var studentRow = new student_import()
                        {
                            student_name = worksheet.Cells[row, 1].Value.ToString(),
                            birthday = dateTimeBirthday,
                            gender = worksheet.Cells[row, 3].Value.ToString(),
                            address = worksheet.Cells[row, 4].Value.ToString(),
                            phone_number = worksheet.Cells[row, 5].Value.ToString(),
                            email = worksheet.Cells[row, 6].Value.ToString(),
                            classes_code = worksheet.Cells[row, 7].Value.ToString(),
                        };

                        listStudentImport.Add(studentRow);
                    }
                    if (listStudentImport != null && listStudentImport.Count > 0)
                    {
                        var studentImportFirst = listStudentImport[0];
                        // Lấy classes_id theo classes_code
                        var classesByCode = new classes();
                        if (studentImportFirst != null)
                        {
                            classesByCode = await _classesDL.GetByCodeAsync(studentImportFirst.classes_code);
                        }

                        if (classesByCode == null)
                        {
                            return new ResponseService
                            (
                                StatusCodes.Status400BadRequest,
                                "Mã lớp học không tồn tại trong hệ thống",
                                new Dictionary<string, object>
                                {
                                    { "message_error", "Mã lớp học không tồn tại trong hệ thống" }
                                }
                            );
                        } 

                        foreach (var student in listStudentImport)
                        { 
                            student studentItemSave = new student()
                            {
                                student_id = Guid.NewGuid(),
                                classes_id = classesByCode.classes_id,
                                student_code = BNTUtil.GenerateCode(),
                                student_name = student.student_name,
                                birthday = student.birthday,
                                gender = student.gender,
                                address = student.address,
                                phone_number = student.phone_number,
                                email = student.email,
                                created_by = "Bùi Ngọc Tiến",
                                created_date = DateTime.Now,
                                modified_by = "",
                                modified_date = DateTime.Now,   
                                admission_year = DateTime.Now
                            };
                            studentSave.Add(studentItemSave);
                        }
                    } 

                    // cất dữ liệu
                    var res = await InsertMultipleAsync(studentSave);
                    return res;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Dictionary<string, object>
                        {
                            { "message_error", ex.Message }
                        }
                    );
            }
        }

        protected override async Task AfterInsertAsync(student entity)
        {
            // Lấy quyền
            var roles = await _roleDL.GetAllAsync();
            var role = roles.FirstOrDefault(x => x.role_code == EnumPermission.Student);

            // Tạo tài khoản đăng nhập
            var userNew = new user()
            {
                user_id = Guid.NewGuid(),
                user_name = entity.student_code,
                pass_word = "1",
                role_id = role.role_id,
                status = 1,
                created_by = "Bùi Ngọc Tiến",
                created_date = DateTime.Now,
                modified_by = "",
                modified_date= DateTime.Now,
            };

            await _userDL.InsertAsync(userNew);
        }

        protected override async Task AfterInsertMultipleAsync(List<student> entities)
        {
            if(entities != null && entities.Count > 0)
            {
                // Lấy quyền
                var roles = await _roleDL.GetAllAsync();
                var role = roles.FirstOrDefault(x => x.role_code == EnumPermission.Student);

                var listUserNew = new List<user>();
                foreach(var s in entities)
                {
                    var userNew = new user()
                    {
                        user_id = Guid.NewGuid(),
                        user_name = s.student_code,
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
                var listStudentExport = await _studentDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);

                var res = await _studentDL.ExportExcelAsync(listStudentExport.Item1);
                return res;
            }
            catch(Exception ex)
            {
                return new MemoryStream();
            }
        }

        public override async Task<ResponseService> DeleteAsync(Guid id)
        {
            try
            {
                // Lấy bản ghi cần xóa theo id
                var studentDelete = await _studentDL.GetByIdAsync(id);

                if(studentDelete != null)
                {
                    // Xóa tài khoản đăng nhập
                    await _userDL.DeleteByUserName(studentDelete.student_code);
                }

                var res = await base.DeleteAsync(id);

                return new ResponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
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

        public override async Task<ResponseService> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                // Lấy các bản ghi cần xóa theo id
                var liststudentDelete = await _studentDL.GetByListIdAsync(ids);

                if (liststudentDelete != null && liststudentDelete.Count > 0)
                {
                    var listUserNameDelete = liststudentDelete.Select(x => x.student_code).ToList();    

                    // Xóa tài khoản đăng nhập
                    await _userDL.DeleteByListUserName(listUserNameDelete);
                }

                var res = await base.DeleteMultipleAsync(ids);
                return new ResponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
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

        protected override void CustomParamSave(student entity, ModelState state)
        {
            if(state == ModelState.Insert && entity.admission_year == null)
            {
                entity.admission_year = DateTime.Now;
            }
        }

        public async Task<ResponseService> MarkGraduatedAsync(Guid id)
        {
            try
            {
                var res = await _studentDL.MarkGraduatedAsync(id);
                return new ResponseService(StatusCodes.Status200OK, "Cập nhật dữ liệu thành công", res);
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
