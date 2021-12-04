using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RING.Areas.RING.Data
{
    public class RegisterTable
    {
        public class Register
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

        }
        public class RegisterView
        {
            public string Error { get; set; }
            public string ResultOk { get; set; }
        }   
    }
}
