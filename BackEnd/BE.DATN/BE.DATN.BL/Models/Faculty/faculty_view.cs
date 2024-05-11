using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Faculty
{
    public class faculty_view
    {
        public Guid faculty_id { get; set; }
        public string faculty_code { set; get; }
        public string faculty_name { set; get; }
    }
}
