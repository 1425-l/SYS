using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private SysInfoEntities7 db = new SysInfoEntities7();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Syssz> users = db.Syssz.ToList();
            ViewBag.u = users[0].Tite.Trim();
            return View();
        }

        public ActionResult Login(string AdminUser, string AdminPwd)
        {
            var strMsg = "";
            var strUser = AdminUser;
            var strPwd = AdminPwd;
            //try
            //{
            var dbAdmin = (from Admin in db.Admin where Admin.AdminUser == strUser.Trim() select Admin).Single();
            if (dbAdmin.AdminPwd.Trim().Equals(strPwd))
            {
                strMsg = "success";
            }
            else
            {
                strMsg = "PassErro";
            }
            //}
            //catch (Exception e)
            //{

            //    strMsg = "usernoexsit";
            //    Console.WriteLine("e");
            //}

            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 起始页
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<Syssz> users = db.Syssz.ToList();
            ViewBag.u = users[0].Tite;
            ViewBag.Message = "Your application description page.";

            return View();

        }
        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 传感器管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Sensorinfo()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 数据日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Datalog()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Syslog()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Createlog(Syssz user, int id)
        {
            Syssz soi = db.Syssz.Find(id);
            //添加
            if (soi == null)
            {
                user.date = DateTime.Now;
                db.Syssz.Add(user);
                db.SaveChanges();

            }
            //修改
            else
            {
                soi.Tite = user.Tite;
                user.date = DateTime.Now;
                db.SaveChanges();
            }


            var obj = new
            {
                success = "true",
                message = "ok"
            };
            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 地图上传
        /// </summary>
        /// <returns></returns>
        public ActionResult Mapup()
        {
            //SaveImage();
            return View();
        }
        /// <summary>
        /// 把图片上传|||到服务器并保存路径到数据库
        /// </summary>
        /// <returns></returns>
        public string SaveImage()
        {
            string result = "";
            HttpPostedFileBase imageName = Request.Files["file"];// 从前台获取文件
            string file = imageName.FileName;
            string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
            Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
            if (string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
            {
                result = "error";
            }
            else
            {
                string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                string imageStr = "Content/images/";// 获取保存图片的项目文件夹
                string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
                string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                string saveFile = uploadPath + fileName;//文件路径

                imageName.SaveAs(saveFile);// 保存文件
                                           // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                string image = imageStr + fileName;// 设置数据库保存的路径
                imageStr = image;
                Syssz user = db.Syssz.Find(0);
                user.date = DateTime.Now;
                user.pic = imageStr;
                db.SaveChanges();

                ViewData["qw"] = image;
                //  = image;
                result = "success";

            }
            return result;
        }
        string imagestr = "";
        public ActionResult Mapupdate()
        {
            Pic pic = new Pic();
            pic.PicUrl = imagestr;
            db.Pic.Add(pic);
            db.SaveChanges();

            var obj = new
            {
                success = "true",
                message = "ok"
            };
            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);

        }

        #region MyRegion
        //public string SaveImage()//public string ImageUpLoad()
        //{
        //    HttpPostedFileBase hp = Request.Files["upImage"];
        //    string uploadPath = Server.MapPath("~/images");
        //    string fileName = DateTime.Now.ToLongDateString() + System.IO.Path.GetExtension(hp.FileName);
        //    string saveFile = uploadPath + fileName;
        //    hp.SaveAs(saveFile);
        //    return "success";
        //{
        //    string result = "";
        //    HttpPostedFileBase imageName = Request.Files["image"];// 从前台获取文件
        //    string file = imageName.FileName;
        //    string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
        //    Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
        //    if (string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
        //    {
        //        result = "error";
        //    }
        //    else
        //    {
        //        string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
        //        string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
        //        string imageStr = "~/Content/images/"; // 获取保存图片的项目文件夹
        //        string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
        //        string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
        //        string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
        //        string saveFile = uploadPath + fileName;//文件路径
        //        imageName.SaveAs(saveFile);// 保存文件
        //        // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
        //        string image = imageStr + fileName;// 设置数据库保存的路径

        //        result = "success";
        //    }
        //    return result;
        //}//  [HttpPost]  
        //public ActionResult UploadImg()  
        //{  
        //    if (Request.Files.Count > 0)  
        //    {  
        //        HttpPostedFileBase f =Request.Files["file1"];  
        //        f.SaveAs(@"D:\" + f.FileName);  
        //    }  
        //    return View();  
        //}  //[HttpPost] 

        #endregion



    }
}