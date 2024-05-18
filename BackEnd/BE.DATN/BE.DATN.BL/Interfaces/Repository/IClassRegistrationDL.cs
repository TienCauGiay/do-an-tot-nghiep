﻿using BE.DATN.BL.Enums;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IClassRegistrationDL : IBaseDL<class_registration>
    {
        Task<(List<class_registration_view>?, int?)> GetFilterPagingAsync(int limit, int offset, string textSearch);
        Task<List<class_registration_view>?> GetMultipleByCodeAsync(string code);
    }
}
