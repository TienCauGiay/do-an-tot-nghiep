using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Core;
using BE.DATN.BL.Models.Score; 
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
    public class ScoreDL : BaseDL<score>, IScoreDL
    {
        public ScoreDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }  

        public async Task<List<score_view>?> GetByStudentIdScoreViewAsync(Guid student_id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_student_id", student_id);
            var result = await _unitOfWork.Connection.QueryAsync<score_view>(
                "select * from public.func_get_by_student_id_score_view(:p_student_id)", 
                parameters, 
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction);
            return (List<score_view>?)result;
        }

        public async Task<(List<score_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch, string customFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);
            parameters.Add("p_custom_filter", customFilter);

            var students = new List<score_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from public.func_get_filter_paging_score(:p_limit, :p_offset, :p_text_search, :p_custom_filter); " +
                "select count(sc.score_id) from score sc inner join student st on sc.student_id = st.student_id inner join teacher tc on sc.teacher_id = tc.teacher_id inner join class_registration cr on sc.class_registration_id = cr.class_registration_id inner join subject sb on cr.subject_id = sb.subject_id where (st.student_code ilike '%' || :p_text_search || '%' or st.student_name ilike '%' || :p_text_search || '%' or tc.teacher_code ilike '%' || :p_text_search || '%' or tc.teacher_name ilike '%' || :p_text_search || '%' or sb.subject_code ilike '%' || :p_text_search || '%' or sb.subject_name ilike '%' || :p_text_search || '%') and (" + customFilter + ");",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                students = (await multiResult.ReadAsync<score_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (students, totalRecord);
        }

        public async Task<(List<score_view>?, int?)> GetFilterPagingByRoleAsync(int limit, int offset, string textSearch, string customFilter, Guid user_id, string role_code)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_limit", limit);
            parameters.Add("p_offset", offset);
            parameters.Add("p_text_search", textSearch);
            parameters.Add("p_custom_filter", customFilter);
            parameters.Add("p_user_id", user_id);

            var students = new List<score_view>();
            int? totalRecord = 0;

            using (var multiResult = await _unitOfWork.Connection.QueryMultipleAsync(
                "select * from public.func_get_filter_paging_by_role_score(:p_limit, :p_offset, :p_text_search, :p_custom_filter, :p_user_id); " +
                "select count(sc.score_id) from score sc inner join student st on sc.student_id = st.student_id inner join public.user u on st.student_code = u.user_name inner join teacher tc on sc.teacher_id = tc.teacher_id inner join class_registration cr on sc.class_registration_id = cr.class_registration_id inner join subject sb on cr.subject_id = sb.subject_id where (st.student_code ilike '%' || :p_text_search || '%' or st.student_name ilike '%' || :p_text_search || '%' or tc.teacher_code ilike '%' || :p_text_search || '%' or tc.teacher_name ilike '%' || :p_text_search || '%' or sb.subject_code ilike '%' || :p_text_search || '%' or sb.subject_name ilike '%' || :p_text_search || '%') and (" + customFilter + ") and u.user_id = :p_user_id;",
                parameters,
                commandType: CommandType.Text,
                transaction: _unitOfWork.Transaction))
            {
                // Đọc danh sách sinh viên
                students = (await multiResult.ReadAsync<score_view>()).ToList();

                // Đọc totalRecord
                totalRecord = await multiResult.ReadFirstOrDefaultAsync<int>();
            }

            return (students, totalRecord);
        }

        public async Task<List<condition_data>?> GetOptionFilterAsync(EnumOptionFilter optionFilter, string textSearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_option_filter", optionFilter);
            parameters.Add("p_text_search", textSearch);

            var res = await _unitOfWork.Connection.QueryAsync<condition_data>
                (
                "select * from public.function_get_option_filter_score(:p_option_filter, :p_text_search)",
                parameters,
                _unitOfWork.Transaction
                );
            return res.ToList();
        }

        public async Task<MemoryStream> ExportExcelAsync(List<score_view>? listScoreExport)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add("Danh_Sach_Diem");

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:K1"].Merge = true;
                worksheet.Cells["A1:K1"].Value = "Danh sách điểm";
                worksheet.Cells["A1:K1"].Style.Font.Bold = true;
                worksheet.Cells["A1:K1"].Style.Font.Size = 24;
                worksheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:K3"].Style.Font.Bold = true;
                worksheet.Cells["A3"].Value = "STT";
                worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B3"].Value = "Mã sinh viên";
                worksheet.Cells["C3"].Value = "Tên sinh viên";
                worksheet.Cells["D3"].Value = "Tên môn học";
                worksheet.Cells["E3"].Value = "Số tín chỉ";
                worksheet.Cells["F3"].Value = "Tên giảng viên";
                worksheet.Cells["G3"].Value = "Điểm chuyên cần";
                worksheet.Cells["H3"].Value = "Điểm kiểm tra";
                worksheet.Cells["I3"].Value = "Điểm thi";
                worksheet.Cells["J3"].Value = "Điểm trung bình";
                worksheet.Cells["K3"].Value = "Trạng thái";

                if (listScoreExport != null && listScoreExport.Count > 0)
                {
                    // Ghi dữ liệu
                    for (int i = 0; i < listScoreExport.Count; i++)
                    {
                        var sc = listScoreExport[i];
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                        worksheet.Cells["B" + rowIndex].Value = sc.student_code;
                        worksheet.Cells["C" + rowIndex].Value = sc.student_name;
                        worksheet.Cells["D" + rowIndex].Value = sc.subject_name;
                        worksheet.Cells["E" + rowIndex].Value = sc.number_tc;
                        worksheet.Cells["F" + rowIndex].Value = sc.teacher_name;
                        worksheet.Cells["G" + rowIndex].Value = sc.score_attendance;
                        worksheet.Cells["H" + rowIndex].Value = sc.score_test;
                        worksheet.Cells["I" + rowIndex].Value = sc.score_exam;
                        worksheet.Cells["J" + rowIndex].Value = sc.score_average;
                        worksheet.Cells["K" + rowIndex].Value = sc.evaluate_state_name;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                    }

                    // Set border cho các cột và hàng
                    var dataRange = worksheet.Cells["A3:K" + (listScoreExport.Count + 3)];
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    // Đặt chiều rộng cột tự động hiển thị đủ nội dung
                    worksheet.Cells["A:K"].AutoFitColumns();
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
