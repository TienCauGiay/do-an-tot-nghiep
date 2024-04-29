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

        public ScoreBL(IScoreDL scoreDL, IUnitOfWork unitOfWork, IStudentDL studentDL, ITeacherDL teacherDL,
            IClassRegistrationDL classRegistrationDL) : base(scoreDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _scoreDL = scoreDL;
            _studentDL = studentDL;
            _teacherDL = teacherDL;
            _classRegistrationDL = classRegistrationDL;
        }

        protected override void ValidateBusiness(score entity, ModelState state)
        {

        }

        protected override void ValidateBusinessMultiple(List<score> entities, ModelState statte)
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
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }
                if (customFilter == null)
                {
                    customFilter = "1 = 1";
                }
                var res = await _scoreDL.GetFilterPagingAsync(limit, offset, textSearch, customFilter);
                return new ResponseServiceScore()
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
                                score_average = (score.score_attendance + score.score_test * 2 + score.score_exam * 3) / 6,
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
                                    new Object()
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
                                    new Object()
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
                                    new Object()
                                );
                            }
                            scoreSave.Add(scoreItemSave);
                        }
                    }
                    // cất dữ liệu
                    var res = await _scoreDL.InsertMultipleAsync(scoreSave);
                    return new ResponseService(StatusCodes.Status200OK, "Nhập khẩu điểm thành công", res);
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
    }
}
