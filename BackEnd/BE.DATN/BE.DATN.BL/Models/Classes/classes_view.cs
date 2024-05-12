using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Classes
{
    public class classes_view
    {
        public Guid classes_id { get; set; }
        public string classes_code { set; get; }
        public string classes_name { set; get; }
        public Guid faculty_id { get; set; }
        public string faculty_name { set; get;}
    }
}
