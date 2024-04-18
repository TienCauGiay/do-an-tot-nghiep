﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Response
{
    public class ReponseLogin
    {
        public int Code { get; set; }
        public string? Message { get; set; } 
        public string? Token { get; set; }

        public ReponseLogin()
        {
            Code = StatusCodes.Status500InternalServerError;
            Message = "Đã có lỗi xảy ra, vui lòng liên hệ BNTIEN";
            Token = null;
        }

        public ReponseLogin(int code, string? message, string? token)
        {
            Code = code;
            Message = message;
            Token = token;
        }
    }
}
