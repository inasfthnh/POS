﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosDataModel
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
