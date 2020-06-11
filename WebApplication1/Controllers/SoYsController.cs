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
    public class SoYsController : Controller
    {
        private SysInfoEntities7 db = new SysInfoEntities7();

        // GET: SoYs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteU(List<int?> SoyID)
        {
            foreach (int i in SoyID)
            {
                SoY user = db.SoY.Find(i);
                db.SoY.Remove(user);
            }
            db.SaveChanges();
            return Content("success");
        }

        public ActionResult GetSoyList1(string timestar = "1990-1-1 0:0:0.000", string timestop = "2019-10-25 10:46:11.000",string UserName = "",string IP ="", int page = 1, int rows = 10,
            string sort = "SoyID", string order = "desc")
        {

            DateTime timestar1 = DateTime.Parse(timestar);
            DateTime timestop1 = DateTime.Parse(timestop);

            IQueryable<SoY> soy = (from d in db.SoY where d.SoYTime >= timestar1 && d.SoYTime <= timestop1 select d).AsQueryable();


            if (string.IsNullOrWhiteSpace(UserName) && string.IsNullOrWhiteSpace(IP))
            {
                soy = (from d in db.SoY where d.SoYTime >= timestar1 && d.SoYTime <= timestop1 select d).AsQueryable();
                //soy = db.SoY.AsQueryable();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {

                    soy = db.SoY.Where(u => u.SenIP.ToString().Contains(IP) && u.SoYTime >= timestar1 && u.SoYTime <= timestop1).AsQueryable();
                }
                else if (string.IsNullOrWhiteSpace(IP))
                {
                    soy = db.SoY.Where(u => u.SenZt.ToString().Contains(UserName) && u.SoYTime >= timestar1 && u.SoYTime <= timestop1).AsQueryable();
                }
                else
                {
                    soy = db.SoY.Where(u => u.SenZt.ToString().Contains(UserName) && u.SenIP.ToString().Contains(IP) && u.SoYTime >= timestar1 && u.SoYTime <= timestop1).AsQueryable();
                }
            }
            //paixu
            if (order == "desc")
            {
                switch (sort)
                {
                    case "SoyID":
                        soy = soy.OrderByDescending(u => u.SoyID);
                        break;
                    case "SenID":
                        soy = soy.OrderByDescending(u => u.SenID);
                        break;
                    case "SenIP":
                        soy = soy.OrderByDescending(u => u.SenIP);
                        break;
                    case "SenZt":
                        soy = soy.OrderByDescending(u => u.SenZt);
                        break;
                    case "SoYTime":
                        soy = soy.OrderByDescending(u => u.SoYTime);
                        break;
                    default:
                        soy = soy.OrderByDescending(u => u.UserID);
                        break;
                }
            }
            else
            {

                switch (sort)
                {
                    case "SoyID":
                        soy = soy.OrderBy(u => u.SoyID);
                        break;
                    case "SenID":
                        soy = soy.OrderBy(u => u.SenID);
                        break;
                    case "SenIP":
                        soy = soy.OrderBy(u => u.SenIP);
                        break;
                    case "SenZt":
                        soy = soy.OrderBy(u => u.SenZt);
                        break;
                    case "SoYTime":
                        soy = soy.OrderBy(u => u.SoYTime);
                        break;
                    default:
                        soy = soy.OrderBy(u => u.UserID);
                        break;
                }
            }

            //分页
            int total = soy.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    soy = soy.Take(rows);
                }
                else
                {
                    soy = soy.Skip((page - 1) * rows).Take(rows);
                }
            }
            ///zhushi
            db.Configuration.ProxyCreationEnabled = false;

            var obj = new
            {
                total = total,
                rows = soy.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);



        }
        #region MyRegion
        //public ActionResult CreateUser(User user)
        //{
        //    user.Logintime = DateTime.Now;
        //    db.User.Add(user);
        //    db.SaveChanges();

        //    var obj = new
        //    {
        //        success = "true",
        //        message = "ok"
        //    };
        //    return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        //}
        //public ActionResult DeleteU(List<int?> UserID)
        //{
        //    foreach (int i in UserID)
        //    {
        //        User user = db.User.Find(i);
        //        db.User.Remove(user);
        //    }
        //    db.SaveChanges();
        //    return Content("success");
        //}
        //public ActionResult EditUser(int? UserID, string username, int useracc, string userpwd)
        //{

        //    User user = db.User.Find(UserID);
        //    user.UserAcc = useracc;
        //    user.UserName = username;
        //    user.UserPwd = userpwd;

        //    db.SaveChanges();

        //    var obj = new
        //    {
        //        success = "true",
        //        message = "ok"
        //    };
        //    return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        //}
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.User.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit1([Bind(Include = "UserID,UserName,UserAcc,UserPwd,Logintime")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }


            base.Dispose(disposing);
        }
        // GET: SoYs/Details/5

    }
}
