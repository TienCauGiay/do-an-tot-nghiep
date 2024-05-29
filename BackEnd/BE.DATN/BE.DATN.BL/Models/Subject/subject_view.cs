using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Subject
{
    public class subject_view
    {
        public Guid subject_id { get; set; }
        public string subject_code { set; get; }
        public string subject_name { set; get; }
        public Guid semester_id { get; set; }
        public string semester_name { set; get; }
        public int number_tc { get; set; }
    }
}
