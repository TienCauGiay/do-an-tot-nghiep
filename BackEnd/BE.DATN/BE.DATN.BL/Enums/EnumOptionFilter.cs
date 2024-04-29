using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Enums
{
    public enum EnumOptionFilter : int
    {
        // Không xác định
        None = 0,
        // Lọc theo lớp
        Class = 1,
        // Lọc theo giới tính
        Gender = 2,
        // Lọc theo tỉnh
        Address = 3,
        // Lọc theo khoa 
        Faculty = 4,
        // Lọc theo mã sinh viên
        StudentCode = 5,
        // Lọc theo tên sinh viên
        StudentName = 6,
        // Lọc theo tên môn học
        SubjectName = 7,
        // Lọc theo tên giảng viên
        TeacherName = 8,
    }
}
