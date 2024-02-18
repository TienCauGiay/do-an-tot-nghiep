using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Semester
{
    public class semester : BaseModel
    {
        public Guid semester_id { get; set; }
        public string semester_code { get; set; }
        public string semester_name { get; set; }
    }
}
