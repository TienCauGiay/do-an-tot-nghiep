using BE.DATN.BL.Models.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Response
{
    public class ResponseServiceFaculty
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
        public List<faculty_view>? Data { get; set; }
        public ResponseServiceFaculty()
        {
            Data = new List<faculty_view>();
        }
    }
}
