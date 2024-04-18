using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Enums
{
    /// <summary>
    /// Enum xác định trạng thái bản ghi
    /// </summary>
    public enum ModelState : int
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        None = 0,
        /// <summary>
        /// Thêm mới
        /// </summary>
        Insert = 1,
        /// <summary>
        /// Sửa
        /// </summary>
        Update = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3,
    }
}
