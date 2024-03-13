using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.User
{
    public class user : BaseModel
    {
        public Guid user_id { get; set; }
        public string user_name { get; set; }
        public string? pass_word { get; set; }
        public Guid role_id { get; set; }
        public int status { get; set; }
    }
}
