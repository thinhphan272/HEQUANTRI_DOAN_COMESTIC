using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Admin/Exception
        public ActionResult Forbidden()
        {
            return View();
        }
    }
}