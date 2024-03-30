﻿using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class ClassRegistrationDL : BaseDL<class_registration>, IClassRegistrationDL
    {
        public ClassRegistrationDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
