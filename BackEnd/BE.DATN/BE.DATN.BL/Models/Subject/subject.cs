using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Subject
{
    public class subject : BaseModel
    {
        public Guid subject_id { get; set; }
        public Guid semester_id { get; set; }
        public string subject_code { get; set; }
        public string subject_name { get; set; }
        public int number_tc { get; set; }
        public int score_rate { get; set; }
    }
}
