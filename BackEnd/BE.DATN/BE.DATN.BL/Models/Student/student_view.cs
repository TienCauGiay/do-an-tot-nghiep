﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Student
{
    public class student_view
    {
        public Guid student_id { get; set; }     
        public string student_code { get; set; }
        public string student_name { get; set;}
        public string classes_code { get; set; }
        public string classes_name { get; set;}
        public DateTime? birthday { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
    }
}