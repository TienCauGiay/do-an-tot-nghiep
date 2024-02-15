using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Faculty
{
    public class faculty : BaseModel
    {
        public Guid faculty_id { get; set; }
        public string faculty_code { get; set; }
        public string faculty_name { get; set; }
    }
}
