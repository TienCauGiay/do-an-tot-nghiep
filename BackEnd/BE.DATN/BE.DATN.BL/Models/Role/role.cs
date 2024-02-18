using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Role
{
    public class role : BaseModel
    {
        public Guid role_id { get; set; }
        public string role_code { get; set; }
        public string description { get; set; }
    }
}
