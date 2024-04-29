using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Teacher
{
    public class teacher_import
    {
        public string? teacher_name { get; set; }
        public DateTime? birthday { get; set; }
        public string? gender { get; set; }
        public string? phone_number { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? faculty_code { get; set; }
    }
}
