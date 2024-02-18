using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Teacher
{
    public class teacher : BaseModel
    {
        public Guid teacher_id { get; set; }
        public Guid subject_id { get; set; }
        public string teacher_code { get; set; }
        public string teacher_name { get; set; }
        public DateTime? birthday { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string image { get; set; }
    }
}
