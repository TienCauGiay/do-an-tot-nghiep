using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.ClassRegistration
{
    public class class_registration : BaseModel
    {
        public Guid class_registration_id { get; set; }
        public Guid subject_id { get; set; }
        public string class_registration_code { get; set; }
        public string class_registration_name { get; set; } 
    }
}
