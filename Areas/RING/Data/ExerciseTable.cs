using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RING.Areas.RING.Data
{
    public class ExerciseTable
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}