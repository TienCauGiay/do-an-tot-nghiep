﻿using BE.DATN.BL.Models.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IFacultyDL : IBaseDL<faculty>
    {
        Task<(List<faculty_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);
    }
}
