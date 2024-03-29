﻿using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class ClassesDL : BaseDL<classes>, IClassesDL
    {
        public ClassesDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
