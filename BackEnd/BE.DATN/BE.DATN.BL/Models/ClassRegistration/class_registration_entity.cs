using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.ClassRegistration
{
    public class class_registration_entity : class_registration
    {
        public List<class_registration_detail>? ListDetail { get; set; }
    }
}
