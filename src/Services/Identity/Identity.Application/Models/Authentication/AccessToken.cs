﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models.Authentication
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long ExpDate { get; set; }
    }
}
