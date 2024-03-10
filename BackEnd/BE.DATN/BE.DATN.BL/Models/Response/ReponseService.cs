﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Models.Response
{
    public class ReponseService
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public Object? Data { get; set; }
        public ReponseService(int Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }
        public ReponseService(int Code, string Message, Object Data)
        {
            this.Code = Code;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
