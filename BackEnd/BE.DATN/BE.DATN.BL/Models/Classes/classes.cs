using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Classes
{
    public class classes : BaseModel
    {
        public Guid classes_id { get; set; }
        public Guid faculty_id { get; set; }
        public string classes_code { get; set; }
        public string classes_name { get; set; }
    }
}
