﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CustomHandler
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
