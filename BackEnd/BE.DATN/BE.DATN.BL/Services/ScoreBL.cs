using BE.DATN.BL.Common;
using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Score;
using BE.DATN.BL.Models.Student;
using BE.DATN.BL.Models.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace BE.DATN.BL.Services
{
    public class ScoreBL : BaseBL<score>, IScoreBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IScoreDL _scoreDL;

        private readonly IStudentDL _studentDL;

        private readonly ITeacherDL _teacherDL;

        private readonly IClassRegistrationDL _classRegistrationDL;

        private readonly ITokenService _tokenService;

        private readonly IConfiguration _configuration;

        private Guid _userId = Guid.Empty;

        private string _roleCode = "";

        public ScoreBL(IScoreDL scoreDL, 
            IStudentDL studentDL, 
            ITeacherDL teacherDL,
            IClassRegistrationDL classRegistrationDL,
            IUnitOfWork unitOfWork, 
            ITokenService tokenService,
            IConfiguration configuration
            ) : base(scoreDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _scoreDL = scoreDL;
            _studentDL = studentDL;
            _teacherDL = teacherDL;
            _classRegistrationDL = classRegistrationDL;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        protected override void ValidateBusiness(score entity, ModelState state)
        {

        } 

        protected override void ValidateBeforeDelete(Guid id)
        {

        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {

        }

        public async Task<ResponseServiceScore> GetFilterPagingAsync(int limit, int offset, string? textSearch, string? customFilter)
        {
            try
            {
                var token = _tokenService.GetToken();
                var jwtService = new JwtService(_configuration["JwtSettings:SecretKey"], _configuration);

                // Giải mã token
                var principal = jwtService.ValidateToken(token);

                // Truy cập thông tin từ các claims
                if (principal != null)
                {
                    _userId = Guid.Parse(principal.FindFirst("user_id")?.Value);
                    _roleCode = principal.FindFirst("role_code")?.Value; 
                }

                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }
                if (customFilter == null)
                {
                    customFilter = "1 = 1";
                }
                List<score_view>? data = null;
                int? totalRecord = null;
                if (_roleCode == EnumPermission.Student.ToString())
                {
                    var res = await _scoreDL.GetFilterPagingByRoleStudentAsync(limit, offset, textSearch, customFilter, _userId, _roleCode);
                    data = res.Item1;
                    totalRecord = res.Item2;
                } else if (_roleCode == EnumPermission.Teacher.ToString())
                {
                    var res = await _scoreDL.GetFilterPagingByRoleTeacherAsync(limit, offset, textSearch, customFilter, _userId, _roleCode);
                    data = res.Item1;
                    totalRecord = res.Item2;
                }
                else
                { 
                    var res = await _scoreDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
                    data = res.Item1;
                    totalRecord = res.Item2;
                } 
                return new ResponseServiceScore()
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
                return new ResponseServiceScore()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<score_view>()
                };
            }
        }

        public async Task<ResponseService> GetByStudentIdScoreViewAsync(Guid student_id)
        {
            try
            {
                var res = await _scoreDL.GetByStudentIdScoreViewAsync(student_id);
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
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

        public async Task<ResponseService> GetOptionFilterAsync(EnumOptionFilter optionFilter, string? textSearch)
        {
            try
            {
                if (string.IsNullOrEmpty(textSearch))
                {
                    textSearch = string.Empty;
                }
                var res = await _scoreDL.GetOptionFilterAsync(optionFilter, textSearch);
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
                        new Object()
                    );
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Sử dụng EPPlus để đọc tệp Excel
                using (var package = new ExcelPackage(formFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Lấy trang tính đầu tiên

                    var listScoreImport = new List<score_import>();
                    List<score> scoreSave = new List<score>();
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var scoreRow = new score_import()
                        {
                            score_id = Guid.NewGuid(),
                            student_code = worksheet.Cells[row, 1].Value.ToString(),
                            teacher_code = worksheet.Cells[row, 2].Value.ToString(),
                            class_registration_code = worksheet.Cells[row, 3].Value.ToString(),
                            score_attendance = float.Parse(worksheet.Cells[row, 4].Value.ToString()),
                            score_test = float.Parse(worksheet.Cells[row, 5].Value.ToString()),
                            score_exam = float.Parse(worksheet.Cells[row, 6].Value.ToString())
                        };

                        listScoreImport.Add(scoreRow);
                    }
                    if (listScoreImport != null && listScoreImport.Count > 0)
                    {
                        var scoreImportFirst = listScoreImport[0];
                        // Lấy student_id và teacher_id theo student_code và teacher_code 
                        var teacherByCode = new teacher();
                        var classRegistrationByCode = new class_registration();
                        if (scoreImportFirst != null)
                        {
                            teacherByCode = await _teacherDL.GetByCodeAsync(scoreImportFirst.teacher_code);
                            classRegistrationByCode = await _classRegistrationDL.GetByCodeAsync(scoreImportFirst.class_registration_code);
                        } 

                        var listStudentByCode = await _studentDL.GetByListCodeAsync(listScoreImport.Select(x => x.student_code).ToList());

                        foreach (var score in listScoreImport)
                        {
                            score scoreItemSave = new score()
                            {
                                score_id = score.score_id,
                                score_attendance = score.score_attendance,
                                score_test = score.score_test,
                                score_exam = score.score_exam,
                                score_average = (float)Math.Round((score.score_attendance + score.score_test * 2 + score.score_exam * 3) / 6.0, 2),
                                created_date = DateTime.Now,
                                modified_date = DateTime.Now,
                            };
                            if (teacherByCode != null)
                            {
                                scoreItemSave.teacher_id = teacherByCode.teacher_id;
                            }
                            else
                            {
                                return new ResponseService
                                (
                                    StatusCodes.Status400BadRequest,
                                    "Mã giảng viên không tồn tại trong hệ thống",
                                    new Dictionary<string, object>
                                    {
                                        { "message_error", "Mã giảng viên không tồn tại trong hệ thống" }
                                    }
                                );
                            }
                            if (classRegistrationByCode != null)
                            {
                                scoreItemSave.class_registration_id = classRegistrationByCode.class_registration_id;
                            }
                            else
                            {
                                return new ResponseService
                                (
                                    StatusCodes.Status400BadRequest,
                                    "Mã lớp học phần không tồn tại trong hệ thống",
                                    new Dictionary<string, object>
                                    {
                                        { "message_error", "Mã lớp học phần không tồn tại trong hệ thống" }
                                    }
                                );
                            }
                            // Tìm sinh viên có student_id tương ứng trong list students
                            var student = new student();
                            if (listStudentByCode != null && listStudentByCode.Count > 0)
                            {
                                student = listStudentByCode.FirstOrDefault(s => s.student_code == score.student_code);
                            }

                            // Kiểm tra xem có sinh viên nào tìm thấy không
                            if (student != null)
                            {
                                // Gán student_code của sinh viên cho student_code của score
                                scoreItemSave.student_id = student.student_id;
                            }
                            else
                            {
                                return new ResponseService
                                (
                                    StatusCodes.Status400BadRequest,
                                    $"Mã sinh viên {score.student_code} không tồn tại trong hệ thống",
                                    new Dictionary<string, object>
                                    {
                                        { "message_error", $"Mã sinh viên {score.student_code} không tồn tại trong hệ thống" }
                                    }
                                );
                            }
                            scoreSave.Add(scoreItemSave);
                        }
                    }
                    // cất dữ liệu
                    var res = await InsertMultipleAsync(scoreSave);
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

        protected override async Task<Dictionary<string, object>?> ValidateBusinessMultiple(List<score> entities, ModelState statte)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();

            // Kiểm tra xem sinh các sinh viên có trong lớp học phần đang nhập điểm hay không
            if(entities.Count > 0)
            {
                var classRegistrationId = entities[0].class_registration_id;
                var teacherId = entities[0].teacher_id;
                var listStudentId = String.Join(";", entities.Select(x => x.student_id).ToList());
                var studentNotExistsInClassRegistration = await _classRegistrationDL.CheckExistsInClassRegistraionMultiple(classRegistrationId, teacherId, listStudentId);
                if(studentNotExistsInClassRegistration != null && studentNotExistsInClassRegistration.Count > 0)
                {
                    var listStudentCodeError = String.Join(", ", studentNotExistsInClassRegistration.Select(x => x.student_name).ToList());
                    res.Add("message_error", $"Không có các sinh viên {listStudentCodeError} trong lớp học phần");
                }
                else
                {
                    var studentNotExistsInScore = await _scoreDL.CheckExistsInScoreMultiple(classRegistrationId, teacherId, listStudentId);
                    if(studentNotExistsInScore != null && studentNotExistsInScore.Count > 0)
                    {
                        var listStudentCodeError2 = String.Join(", ", studentNotExistsInScore.Select(x => x.student_name).ToList());
                        res.Add("message_error", $"Các sinh viên {listStudentCodeError2} đã có điểm của lớp học phần");
                    }
                }
            }  

            if (res.Count > 0)
            {
                return res;
            }
            else
            {
                return null;
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
                var listStudentExport = await _scoreDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);

                var res = await _scoreDL.ExportExcelAsync(listStudentExport.Item1);
                return res;
            }
            catch (Exception ex)
            {
                return new MemoryStream();
            }
        }

        protected override async Task AfterInsertAsync(score entity)
        {
            
        }

        public async Task<ResponseService> CheckExistsStudentInClassRegitrationAsync(score entity)
        {
            try
            {
                Dictionary<string, bool> dict = new Dictionary<string, bool>();
                var existInClassRegistration = await _classRegistrationDL.CheckExistsInClassRegistraion(entity.class_registration_id, entity.teacher_id, entity.student_id);
                var existsInScore = await _scoreDL.CheckExistsInScore(entity);
                dict.Add("ExistInClassRegistration", existInClassRegistration);
                dict.Add("ExistsInScore", existsInScore);

                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", dict);
            }
            catch(Exception ex)
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
