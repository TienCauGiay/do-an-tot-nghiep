using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BE.DATN.BL.Common
{
    public class BNTUtil
    {
        public static string GenerateCode(string fullName, DateTime? birthday)
        {
            // Lấy năm hiện tại
            int currentYear = DateTime.Now.Year;

            // Lấy những chữ cái đầu trong họ tên
            string[] nameParts = fullName.Split(' ');
            string initials = "";
            foreach (string part in nameParts)
            {
                string initial = part[0].ToString();
                if (initial == "Đ" || initial == "đ")
                {
                    initial = "d";
                }
                initials += initial;
            }

            // Lấy ngày, tháng, năm sinh viết liền
            string birthDate = "";
            if (birthday.HasValue)
            {
                birthDate = birthday.Value.ToString("ddMMyyyy");
            }

            // Tạo mã 
            string code = currentYear + "_" + initials.ToLower() + "_" + birthDate;

            return code;
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải tất cả đều là số hay không
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNumber(string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            return !str.All(char.IsDigit);
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Biểu thức chính quy để kiểm tra email hợp lệ
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
