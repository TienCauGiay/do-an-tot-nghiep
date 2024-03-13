using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public class RoleDL : BaseDL<role>, IRoleDL
    {
        public RoleDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
