using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IAuthBL
    {
        Task<ReponseLogin> LoginAsync(user user);
    }
}
