using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.User
{
    public class user_view
    {
        public Guid user_id { get; set; }
        public string user_name { get; set; }
        public Guid role_id { get; set; }
        public string role_name { get; set;}
        public int status { get; set; }
    }
}
