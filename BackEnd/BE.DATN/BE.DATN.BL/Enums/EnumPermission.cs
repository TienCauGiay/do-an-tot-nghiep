using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Enums
{
    /// <summary>
    /// Enum phân quyền người dùng
    /// </summary>
    public enum EnumPermission : int
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        None = 0,
        /// <summary>
        /// Quản trị viên
        /// </summary>
        Admin = 1,
        /// <summary>
        /// Sinh viên
        /// </summary>
        Student = 2,
        /// <summary>
        /// Giảng viên
        /// </summary>
        Teacher = 3,
    }
}
