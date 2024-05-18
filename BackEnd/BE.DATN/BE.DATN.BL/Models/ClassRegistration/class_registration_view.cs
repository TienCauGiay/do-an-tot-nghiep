using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.ClassRegistration
{
    public class class_registration_view : class_registration
    {
        public string? subject_code { get; set; }
        public string? subject_name { get; set; }
        public string? teacher_code { get; set; }
        public string? teacher_name { get; set; }
        public string? student_code { get; set; }
        public string? student_name { get; set; }
    }
}
