using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Models
{
    public class StaffViewModel
    {
        public string Username { get; set; }
        public bool IsLocked { get; set; } // True: Bị khóa, False: Hoạt động
    }
}