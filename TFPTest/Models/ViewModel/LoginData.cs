﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFPTest.Models.ViewModel
{
    public class LoginData
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}