using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class UsersController : Controller
    {
        
        private SysInfoEntities7 db = new SysInfoEntities7();
        /// <summary>

        /// 16位MD5加密

        /// </summary>

        /// <param name="password"></param>
        
        //public ActionResult MD5Encrypt16(string password)

        //{

        //    //计算输入数据的哈希值

        //    var md5 = new MD5CryptoServiceProvider();

        //    //value:字节数组。 startIndex:value 内的起始位置。length:要转换的 value 中的数组元素数。

        //   string 加密后 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);

        //    加密后 = 加密后.Replace("-", "");

        //    return Json(加密后, JsonRequestBehavior.AllowGet);
        //}
        // GET: Users
        public ActionResult CreateUser(User user)
        {
            var md5 = new MD5CryptoServiceProvider();
            user.Logintime = DateTime.Now;
            user.UserPwd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(user.UserPwd)), 4, 8);
            db.User.Add(user);
            db.SaveChanges();

            var obj = new
            {
                success = "true",
                message = "ok"
            };
            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult SetUser() {

        //}
        public ActionResult GetUserList()
        {
            
            db.Configuration.ProxyCreationEnabled = false;
            List<User> users = db.User.ToList();
            var obj = new
            {
                total = users.Count(),
                rows = users.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);



        }
      
        public ActionResult GetUserList1(string Username="",int page = 1, int rows = 10,
            string sort = "UserID",string order = "desc")
        {
            
            IQueryable<User> users = null;
            if(string.IsNullOrWhiteSpace(Username))
            {
                users = db.User.AsQueryable();
            }
            else
            {
                if(Request["SetUser"] == "1")
                {
                    users = db.User.Where(u => u.UserName.Contains(Username)).AsQueryable();
                }
            }
            
            //paixu
            if (order == "desc")
            {
                switch (sort)
                {
                    case "UserID":
                        users = users.OrderByDescending(u => u.UserID);
                        break;
                    case "UserName":
                        users = users.OrderByDescending(u => u.UserName);
                        break;
                    case "UserPwd":
                        users = users.OrderByDescending(u => u.UserPwd);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.Logintime);
                        break;
                }
            }
            else
            {
                switch (sort)
                {
                    case "UserID":
                        users = users.OrderBy(u => u.UserID);
                        break;
                    case "UserName":
                        users = users.OrderBy(u => u.UserName);
                        break;
                    case "UserPwd":
                        users = users.OrderBy(u => u.UserPwd);
                        break;
                    default:
                        users = users.OrderBy(u => u.Logintime);
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
        public ActionResult DeleteU(List<int?> UserID)
        {
            foreach (int i in UserID)
            {
                User user = db.User.Find(i);
                db.User.Remove(user);
            }
            db.SaveChanges();
            return Content("success");
        }
        public ActionResult EditUser(int? UserID, string username, string userpwd)
        {
            if(userpwd == "")
            {
                var md5 = new MD5CryptoServiceProvider();
                User user = db.User.Find(UserID);
                user.UserName = username;
                user.UserPwd = user.UserPwd;
                db.SaveChanges();
            }
            else
            {
                var md5 = new MD5CryptoServiceProvider();
                User user = db.User.Find(UserID);
                user.UserName = username;
                user.UserPwd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(userpwd)), 4, 8);
                db.SaveChanges();
            }

            var obj = new
            {
                success = "true",
                message = "ok"
            };
            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
      
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
