using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Areas.Admin.Security;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [CheckAthourize(Roles = "Admin, Nhân viên")]
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}