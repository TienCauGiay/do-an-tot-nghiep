using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Score
{
    public class score : BaseModel
    {
        public Guid score_id { get; set; }
        public Guid student_id { get; set; }
        public Guid teacher_id { get; set; }
        public float score_attendance { get; set; }
        public float score_test { get; set; }
        public float score_exam { get; set; }
        public float score_average { get; set; }
    }
}
