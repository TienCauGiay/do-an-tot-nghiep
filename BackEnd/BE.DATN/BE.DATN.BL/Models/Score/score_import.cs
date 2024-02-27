using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Score
{
    public class score_import
    {
        public Guid score_id { get; set; }
        public Guid student_id { get; set; }
        public string student_code { get; set; }
        public Guid teacher_id { get; set; }
        public string teacher_code { get; set; }    
        public float score_attendance { get; set; }
        public float score_test { get; set; }
        public float score_exam { get; set; }
        public float score_average { get; set; }
    }
}
