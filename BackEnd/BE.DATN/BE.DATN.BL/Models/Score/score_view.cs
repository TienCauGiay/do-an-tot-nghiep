using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Score
{
    public class score_view : score
    {
        public string subject_code { get; set; }
        public string subject_name { get; set;}
        public int number_tc { get; set; }
        public string student_code { get; set; }
        public string student_name { get; set;}
        public string teacher_code { get; set; }
        public string teacher_name { get;set; }
        public string class_registration_code { get; set; }
        public string class_registration_name { get; set; }
        public EvaluateState evaluate_state { get; set; }
        public string evaluate_state_name 
        { 
            get
            {
                switch (evaluate_state)
                {
                    case EvaluateState.Pass:
                        return "Đạt";
                    case EvaluateState.Faile:
                        return "Không đạt";
                    default:
                        return "Không xác định";
                }
            }
        }
    }
}
