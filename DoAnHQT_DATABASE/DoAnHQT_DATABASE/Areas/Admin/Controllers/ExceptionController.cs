using DoAnHQT_DATABASE.Areas.Admin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin, Nhân viên")]
    public class ExceptionController : Controller
    {
        // GET: Admin/Exception
        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult Error()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();
            var builder = new SqlConnectionStringBuilder(connectionString);
            ViewBag.DatabaseName = builder.InitialCatalog;
            ViewBag.ServerName = builder.DataSource;

            return View();
        }
    }
}