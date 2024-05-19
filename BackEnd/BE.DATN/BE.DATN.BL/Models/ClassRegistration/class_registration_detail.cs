using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.ClassRegistration
{
    public class class_registration_detail
    {
        public Guid class_registration_detail_id { get; set; }
        public Guid class_registration_id { get; set; }
        public Guid student_id { get; set; }
        public string? student_code { get; set; }
        public string? student_name { get; set; }
    }
}
