using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RING.Areas.RING.Data
{
    public class LoginTable
    {
        public class Login
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginView
        {
            public string Error { get; set; }
            public string ResultOk { get; set; }
        }
    }
}