﻿using BE.DATN.BL.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Response
{
    public class ResponseServiceTeacher
    {
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int? TotalPage { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int? TotalRecord { get; set; }
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int? CurrentPage { get; set; }
        /// <summary>
        /// Số bản ghi trên trang hiện tại
        /// </summary>
        public int? CurrentPageRecords { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
        public List<teacher_view>? Data { get; set; }
        public ResponseServiceTeacher()
        {
            Data = new List<teacher_view>();
        }
    }
}
