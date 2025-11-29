using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    //[CheckAthourize(Roles = "Admin, Nhân viên")]
    public class BackupController : Controller
    {
        // GET: Admin/Backup
        BackupService backupService = new BackupService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BackUpOnSubmit(FormCollection form)
        {
            string backupType = form["backupType"].ToString();
            string databaseName = form["databaseName"].ToString();
            string backupPath = form["backupPath"].ToString();

            try
            {
                int ret = 0;
                if (backupType.Equals("full"))
                {
                    ret = backupService.BackUpFull(databaseName, backupPath);
                }
                else if (backupType.Equals("differential"))
                {
                    ret = backupService.BackUpDifferential(databaseName, backupPath);
                }
                else if (backupType.Equals("log"))
                {
                    ret = backupService.BackUpLog(databaseName, backupPath);
                }

                if (ret != 0)
                {
                    ViewBag.BackupSuccess = "Backup thành công";
                    return View("Index");
                }
                {
                    ViewBag.BackupError = "Backup không thành công";
                    return View("Index");
                }
            }
            catch(Exception e)
            {
                ViewBag.BackupError = $"Backup không thành công {e.Message}";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult RestoreOnSubmit(FormCollection form)
        {
            string restoreType = form["restoreType"].ToString();
            string restoreDatabaseName = form["restoreDatabaseName"].ToString();
            string restorePath = form["restorePath"].ToString();
            int fileNumber = int.Parse(form["fileNumber"]);
            int recovery = int.Parse(form["recovery"]);

            try
            {
                if (fileNumber < 0 || recovery < 0)
                {
                    ViewBag.BackupError = "Restore không thành công";
                    return View("Index");
                }

                int ret = 0;
                if (restoreType.Equals("full") || restoreType.Equals("differential"))
                {
                    ret = backupService.RestoreDataBase(restoreDatabaseName, restorePath, recovery, fileNumber);
                }
                else if (restoreType.Equals("log"))
                {
                    ret = backupService.RestoreLog(restoreDatabaseName, restorePath, recovery, fileNumber);
                }

                if (ret != 0)
                {
                    ViewBag.BackupSuccess = "Restore thành công";
                    return View("Index");
                }
                {
                    ViewBag.BackupError = "Restore không thành công";
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.BackupError = $"Restore không thành công {e.Message}";
                return View("Index");
            }
        }


    }
}