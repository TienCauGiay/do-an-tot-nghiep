using BE.DATN.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.User
{
    public class login
    {
        public Guid user_id;
        public string user_name ;
        public string pass_word;
        public EnumPermission role_code;
        public int status;
    }
}
