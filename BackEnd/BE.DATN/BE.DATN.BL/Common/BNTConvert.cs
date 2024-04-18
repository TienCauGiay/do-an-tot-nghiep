using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Common
{
    public class BNTConvert
    {
        public static string ConvertFromBase64(string? textConvert)
        {
            if (textConvert == null)
            {
                textConvert= string.Empty;  
            }
            // Chuyển đổi từ base64 thành byte array
            byte[] bytes = Convert.FromBase64String(textConvert);
            // Chuyển byte array thành chuỗi sử dụng mã hóa UTF-8
            string resultString = Encoding.UTF8.GetString(bytes);
            return resultString;
        }
    }
}
