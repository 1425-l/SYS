using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SysController : Controller
    {
        private SysInfoEntities7 db = new SysInfoEntities7();

        // GET: Sys
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUserList()
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<Sys> users = db.Sys.ToList();
            var obj = new
            {
                total = users.Count(),
                rows = users.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);



        }
        public ActionResult GetUserList1(/*DateTime date1 = new DateTime(2007, 1, 1, 21, 21, 21),*/ string Userip = "", string HName = "", int page = 1, int rows = 10,
           string sort = "SysID", string order = "desc")
        {

            IQueryable<Sys> users = null;
            if (string.IsNullOrWhiteSpace(HName) && string.IsNullOrWhiteSpace(Userip))
            {
                users = db.Sys.AsQueryable();
            }
            else
            {
                if(string.IsNullOrWhiteSpace(HName))
                {
                    users = db.Sys.Where(u => u.UserIP.Contains(Userip)).AsQueryable();
                }else if(string.IsNullOrWhiteSpace(Userip))
                {
                    users = db.Sys.Where(u => u.HName.Contains(HName)).AsQueryable();
                }else
                {
                    users = db.Sys.Where(u => u.HName.Contains(HName) && u.UserIP.Contains(Userip)).AsQueryable();
                }
                
            }
           
            //paixu
            if (order == "desc")
            {
                switch (sort)
                {
                    case "SysID":
                        users = users.OrderByDescending(u => u.SysID);
                        break;
                    case "HName":
                        users = users.OrderByDescending(u => u.HName);
                        break;
                    case "SysTime":
                        users = users.OrderByDescending(u => u.SysTime);
                        break;
                    case "UserID":
                        users = users.OrderByDescending(u => u.UserID);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.UserIP);
                        break;
                }
            }
            else
            {
                switch (sort)
                {
                    case "SysID":
                        users = users.OrderBy(u => u.SysID);
                        break;
                    case "HName":
                        users = users.OrderBy(u => u.HName);
                        break;
                    case "SysTime":
                        users = users.OrderBy(u => u.SysTime);
                        break;
                    case "UserID":
                        users = users.OrderBy(u => u.UserID);
                        break;
                    default:
                        users = users.OrderBy(u => u.UserIP);
                        break;
                }
            }
            //分页
            int total = users.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    users = users.Take(rows);
                }
                else
                {
                    users = users.Skip((page - 1) * rows).Take(rows);
                }
            }
            ///zhushi
            db.Configuration.ProxyCreationEnabled = false;

            var obj = new
            {
                total = total,
                rows = users.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);



        }
        public ActionResult EditUser(int? SysID, string HName, int UserID, string UserIP)
        {

            Sys user = db.Sys.Find(SysID);
            user.HName = HName;
            user.UserID = UserID;
            user.UserIP = UserIP;

            db.SaveChanges();

            var obj = new
            {
                success = "true",
                message = "ok"
            };
            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        }

        public ActionResult CreateUser(Sys user)
        {
            user.SysTime = DateTime.Now;
            db.Sys.Add(user);
            db.SaveChanges();

            var obj = new
            {
                success = "true",
                message = "ok"
            };
            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        }
        public ActionResult DeleteU(List<int?> SysID)
        {
            foreach (int i in SysID)
            {
                Sys user = db.Sys.Find(i);
                db.Sys.Remove(user);
            }
            db.SaveChanges();
            return Content("success");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
