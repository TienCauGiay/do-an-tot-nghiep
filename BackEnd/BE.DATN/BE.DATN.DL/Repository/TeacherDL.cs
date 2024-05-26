using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Teacher;
using Dapper;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BE.DATN.DL.Repository
{
    public class TeacherDL : BaseDL<teacher>, ITeacherDL
    {
        public TeacherDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        } 

        public async Task<(List<teacher_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);
            parameters.Add("p_custom_filter", customFilter);

            var teachers = new List<teacher_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_teacher(:p_limit, :p_offset, :p_text_search, :p_custom_filter); " +
                "select count(tc.teacher_id) from teacher tc left join faculty f on tc.faculty_id = f.faculty_id where (tc.teacher_code ilike '%' || :p_text_search || '%' or tc.teacher_name ilike '%' || :p_text_search || '%') and (" + customFilter + ");",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                teachers = (await multiResult.ReadAsync<teacher_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (teachers, totalRecord);
        }

        public async Task<List<condition_data>?> GetOptionFilterAsync(EnumOptionFilter optionFilter, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_option_filter", optionFilter);
            parameters.Add("p_text_search", textSearch);

            var res = await _unitOfWork.Connection.QueryAsync<condition_data>
                (
                "select * from public.function_get_option_filter_teacher(:p_option_filter, :p_text_search)",
                parameters,
                _unitOfWork.Transaction
                );
            return res.ToList();
        }

        public async Task<MemoryStream> ExportExcelAsync(List<teacher_view>? listTeacherExport)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add("Danh_Sach_Giang_Vien");

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["A1:H1"].Value = "Danh sách giảng viên";
                worksheet.Cells["A1:H1"].Style.Font.Bold = true;
                worksheet.Cells["A1:H1"].Style.Font.Size = 24;
                worksheet.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:H3"].Style.Font.Bold = true;
                worksheet.Cells["A3"].Value = "STT";
                worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B3"].Value = "Mã giảng viên";
                worksheet.Cells["C3"].Value = "Tên giảng viên";
                worksheet.Cells["D3"].Value = "Giới tính";
                worksheet.Cells["E3"].Value = "Ngày sinh";
                worksheet.Cells["E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["F3"].Value = "Số điện thoại";
                worksheet.Cells["G3"].Value = "Địa chỉ";
                worksheet.Cells["H3"].Value = "Email";

                if (listTeacherExport != null && listTeacherExport.Count > 0)
                {
                    // Ghi dữ liệu
                    for (int i = 0; i < listTeacherExport.Count; i++)
                    {
                        var teacher = listTeacherExport[i];
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                        worksheet.Cells["B" + rowIndex].Value = teacher.teacher_code;
                        worksheet.Cells["C" + rowIndex].Value = teacher.teacher_name;
                        worksheet.Cells["D" + rowIndex].Value = teacher.gender;
                        worksheet.Cells["E" + rowIndex].Value = teacher.birthday;
                        worksheet.Cells["F" + rowIndex].Value = teacher.phone_number;
                        worksheet.Cells["G" + rowIndex].Value = teacher.address;
                        worksheet.Cells["H" + rowIndex].Value = teacher.email;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                        worksheet.Cells["E" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["E" + rowIndex].Style.Numberformat.Format = "dd/MM/yyyy";
                    }

                    // Set border cho các cột và hàng
                    var dataRange = worksheet.Cells["A3:H" + (listTeacherExport.Count + 3)];
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    // Đặt chiều rộng cột tự động hiển thị đủ nội dung
                    worksheet.Cells["A:H"].AutoFitColumns();
                }

                // Setup lưu file
                using (var memoryStream = new MemoryStream())
                {
                    await package.SaveAsAsync(memoryStream);

                    memoryStream.Position = 0;
                    return memoryStream;
                }
            }
        } 

        protected override string BuildQueryCheckArise()
        {
            var query = @"
                select 1 
                from class_registration cr 
                where cr.teacher_id = @Id 
                union 
                select 1 
                from score s 
                where s.teacher_id = @Id 
                limit 1";
            return query;
        }

        protected override string BuildQueryGetIdArise()
        {
            var query = "select * from public.function_get_teacher_id_arise(:p_ids)";
            return query;
        }

        public async Task<teacher?> GetByClassRegistrantionIdAsync(Guid class_registration_id)
        {
            var selectQuery = $"select t.* from teacher t inner join class_registration cr on t.teacher_id = cr.teacher_id where cr.class_registration_id = @Id";

            var parameters = new { Id = class_registration_id };

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<teacher>(selectQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
