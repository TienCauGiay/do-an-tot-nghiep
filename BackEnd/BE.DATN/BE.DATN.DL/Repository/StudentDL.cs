using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Student;
using Dapper;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;

namespace BE.DATN.DL.Repository
{
    public class StudentDL : BaseDL<student>, IStudentDL
    {
        public StudentDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<student_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);
            parameters.Add("p_custom_filter", customFilter);

            var students = new List<student_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from func_get_filter_paging_student(:p_limit, :p_offset, :p_text_search, :p_custom_filter); " +
                "select count(student_id) from student st left join classes cl on st.classes_id = cl.classes_id where (st.student_code ilike '%' || :p_text_search || '%' or st.student_name ilike '%' || :p_text_search || '%') and (" + customFilter + ");", 
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                students = (await multiResult.ReadAsync<student_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (students, totalRecord);
        }


        public async Task<List<student>?> GetByListCodeAsync(List<string> studentCodes)
        {
            var query = "SELECT * FROM student WHERE student_code = ANY(@p_student_codes)";

            var students = await _unitOfWork.Connection.QueryAsync<student>(query, new { p_student_codes = studentCodes.ToArray() }, transaction: _unitOfWork.Transaction);

            return students.ToList();
        }

        public async Task<List<statistic_number_student_view>?> GetStatisticNumberStudentAsync()
        {
            var query = "SELECT * FROM func_get_statistic_number_student()";

            var res = await _unitOfWork.Connection.QueryAsync<statistic_number_student_view>(query, null, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return res.ToList();
        }

        public async Task<List<condition_data>?> GetOptionFilterAsync(EnumOptionFilter optionFilter, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_option_filter", optionFilter);
            parameters.Add("p_text_search", textSearch);

            var res = await _unitOfWork.Connection.QueryAsync<condition_data>
                (
                "select * from public.function_get_option_filter_student(:p_option_filter, :p_text_search)",
                parameters,
                _unitOfWork.Transaction
                );
            return res.ToList();
        }

        public async Task<MemoryStream> ExportExcelAsync(List<student_view>? listStudentExport)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add("Danh_Sach_Sinh_Vien");

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["A1:H1"].Value = "Danh sách sinh viên";
                worksheet.Cells["A1:H1"].Style.Font.Bold = true;
                worksheet.Cells["A1:H1"].Style.Font.Size = 24;
                worksheet.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:H3"].Style.Font.Bold = true;
                worksheet.Cells["A3"].Value = "STT";
                worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B3"].Value = "Mã sinh viên";
                worksheet.Cells["C3"].Value = "Tên sinh viên";
                worksheet.Cells["D3"].Value = "Giới tính";
                worksheet.Cells["E3"].Value = "Ngày sinh";
                worksheet.Cells["E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["F3"].Value = "Số điện thoại";
                worksheet.Cells["G3"].Value = "Địa chỉ";
                worksheet.Cells["H3"].Value = "Email";

                if(listStudentExport != null && listStudentExport.Count > 0)
                {
                    // Ghi dữ liệu
                    for (int i = 0; i < listStudentExport.Count; i++)
                    {
                        var student = listStudentExport[i];
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                        worksheet.Cells["B" + rowIndex].Value = student.student_code;
                        worksheet.Cells["C" + rowIndex].Value = student.student_name;
                        worksheet.Cells["D" + rowIndex].Value = student.gender;
                        worksheet.Cells["E" + rowIndex].Value = student.birthday;
                        worksheet.Cells["F" + rowIndex].Value = student.phone_number;
                        worksheet.Cells["G" + rowIndex].Value = student.address;
                        worksheet.Cells["H" + rowIndex].Value = student.email;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                        worksheet.Cells["E" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["E" + rowIndex].Style.Numberformat.Format = "dd/MM/yyyy";
                    }

                    // Set border cho các cột và hàng
                    var dataRange = worksheet.Cells["A3:H" + (listStudentExport.Count + 3)];
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
    }
}
