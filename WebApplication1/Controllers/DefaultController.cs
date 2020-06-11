using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult grti()
        {
            using (SysInfoEntities7 db = new SysInfoEntities7()) {
                List<User> users = db.User.ToList();
                var obj = new
                {
                    total = users.Count(),
                    rows = users.ToArray()
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}