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
    public class SensorInfoesController : Controller
    {
        private SysInfoEntities7 db = new SysInfoEntities7();
        public string b = "0";
        // GET: SensorInfoes
        public ActionResult Index()
        {
            ViewBag.de = b;
            return View();
        }
        public ActionResult GetSensorInfoList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<User> sensorinfos = db.User.ToList();
            var obj = new
            {
                total = sensorinfos.Count(),
                rows = sensorinfos.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);



        }
        public ActionResult GetSensorInfoList1(string Senzt = "", string IP = "", string ZID = "", string DID = "", int page = 1, int rows = 10,
            string sort = "SenID", string order = "desc")
        {

            IQueryable<SensorInfo> users = null;
            if (string.IsNullOrWhiteSpace(Senzt) && string.IsNullOrWhiteSpace(IP) && string.IsNullOrWhiteSpace(ZID) && string.IsNullOrWhiteSpace(DID))
            {
                users = db.SensorInfo.AsQueryable();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Senzt))
                {
                    if (string.IsNullOrWhiteSpace(IP))
                    {
                        if (string.IsNullOrWhiteSpace(ZID))
                        {
                            users = db.SensorInfo.Where(u => u.DID.Contains(DID)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.DID.Contains(DID) && u.ZID.Contains(ZID)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(DID))
                    {
                        if (string.IsNullOrWhiteSpace(IP))
                        {
                            users = db.SensorInfo.Where(u => u.ZID.Contains(ZID)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.IP.Contains(IP) && u.ZID.Contains(ZID)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(ZID))
                    {
                        if (string.IsNullOrWhiteSpace(DID))
                        {
                            users = db.SensorInfo.Where(u => u.IP.Contains(IP)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.IP.Contains(IP) && u.DID.Contains(DID)).AsQueryable();
                        }
                    }
                    else
                    {
                        users = db.SensorInfo.Where(u => u.DID.Contains(DID) && u.ZID.Contains(ZID) && u.IP.Contains(IP)).AsQueryable();
                    }
                }

                if (string.IsNullOrWhiteSpace(IP))
                {
                    if (string.IsNullOrWhiteSpace(Senzt))
                    {
                        if (string.IsNullOrWhiteSpace(ZID))
                        {
                            users = db.SensorInfo.Where(u => u.DID.Contains(DID)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.DID.Contains(DID) && u.ZID.Contains(ZID)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(DID))
                    {
                        if (string.IsNullOrWhiteSpace(Senzt))
                        {
                            users = db.SensorInfo.Where(u => u.ZID.Contains(ZID)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt) && u.ZID.Contains(ZID)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(ZID))
                    {
                        if (string.IsNullOrWhiteSpace(DID))
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt) && u.DID.Contains(DID)).AsQueryable();
                        }
                    }
                    else
                    {
                        users = db.SensorInfo.Where(u => u.DID.Contains(DID) && u.ZID.Contains(ZID) && u.SenZT.Contains(Senzt)).AsQueryable();
                    }

                }

                if (string.IsNullOrWhiteSpace(ZID))
                {
                    if (string.IsNullOrWhiteSpace(Senzt))
                    {
                        if (string.IsNullOrWhiteSpace(IP))
                        {
                            users = db.SensorInfo.Where(u => u.DID.Contains(DID)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.DID.Contains(DID) && u.IP.Contains(IP)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(DID))
                    {
                        if (string.IsNullOrWhiteSpace(Senzt))
                        {
                            users = db.SensorInfo.Where(u => u.IP.Contains(IP)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt) && u.IP.Contains(IP)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(IP))
                    {
                        if (string.IsNullOrWhiteSpace(DID))
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt) && u.DID.Contains(DID)).AsQueryable();
                        }
                    }
                    else
                    {
                        users = db.SensorInfo.Where(u => u.DID.Contains(DID) && u.ZID.Contains(ZID) && u.SenZT.Contains(Senzt)).AsQueryable();
                    }

                }
                if (string.IsNullOrWhiteSpace(DID))
                {
                    if (string.IsNullOrWhiteSpace(Senzt))
                    {
                        if (string.IsNullOrWhiteSpace(IP))
                        {
                            users = db.SensorInfo.Where(u => u.ZID.Contains(ZID)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.ZID.Contains(ZID) && u.IP.Contains(IP)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(ZID))
                    {
                        if (string.IsNullOrWhiteSpace(Senzt))
                        {
                            users = db.SensorInfo.Where(u => u.IP.Contains(IP)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt) && u.IP.Contains(IP)).AsQueryable();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(IP))
                    {
                        if (string.IsNullOrWhiteSpace(ZID))
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt)).AsQueryable();
                        }
                        else
                        {
                            users = db.SensorInfo.Where(u => u.SenZT.Contains(Senzt) && u.ZID.Contains(ZID)).AsQueryable();
                        }
                    }
                    else
                    {
                        users = db.SensorInfo.Where(u => u.IP.Contains(IP) && u.ZID.Contains(ZID) && u.SenZT.Contains(Senzt)).AsQueryable();
                    }

                }

            }

            //paixu
            if (order == "desc")
            {
                switch (sort)
                {
                    case "SenID":
                        users = users.OrderByDescending(u => u.SenID);
                        break;
                    case "DID":
                        users = users.OrderByDescending(u => u.DID);
                        break;
                    case "UserID":
                        users = users.OrderByDescending(u => u.UserID);
                        break;
                    case "IP":
                        users = users.OrderByDescending(u => u.IP);
                        break;
                    case "SFType":
                        users = users.OrderByDescending(u => u.SFType);
                        break;
                    case "ZID":
                        users = users.OrderByDescending(u => u.ZID);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.SenZT);
                        break;
                }
            }
            else
            {
                switch (sort)
                {
                    case "SenID":
                        users = users.OrderBy(u => u.SenID);
                        break;
                    case "DID":
                        users = users.OrderBy(u => u.DID);
                        break;
                    case "UserID":
                        users = users.OrderBy(u => u.UserID);
                        break;
                    case "IP":
                        users = users.OrderBy(u => u.IP);
                        break;
                    case "SFType":
                        users = users.OrderBy(u => u.SFType);
                        break;
                    case "ZID":
                        users = users.OrderBy(u => u.ZID);
                        break;
                    default:
                        users = users.OrderBy(u => u.SenZT);
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

        public ActionResult CreatSensorInfo(SensorInfo sensorinfo)
        {
            sensorinfo.DID = sensorinfo.DID.Trim();
            sensorinfo.IP = sensorinfo.IP.Trim();
            sensorinfo.SenZT = sensorinfo.SenZT.Trim();
            string ui = sensorinfo.ZID + "q";
            if (ui.Trim() == "q")
            {
                ui = "";
            }

            var obj = new
            {
                success = "true",
                message = "ok"
            };
            List<SensorInfo> sen = (from d in db.SensorInfo where d.DID.Trim() == sensorinfo.DID.Trim() || d.IP.Trim() == sensorinfo.IP.Trim() || d.SenID == sensorinfo.SenID select d).ToList();
            List<SensorInfo> sen2 = (from d in db.SensorInfo where d.ZID.Trim() == sensorinfo.ZID.Trim() && sensorinfo.ZID.Trim() != "" select d).ToList();
            //from d in db.SensorInfo where d.ZID.Trim() == ZID.Trim() && ZID.Trim() != "" select d

            if (sen.Count() == 0 && sen2.Count() < 2)
            {
                adress add = new adress();
                add.SenID = sensorinfo.SenID + "A";
                add.X = 50;
                add.Y = 50;
                db.adress.Add(add);
                
                adress add1 = new adress();
                add1.SenID = sensorinfo.SenID + "B";
                add1.X = 50;
                add1.Y = 50;
                db.adress.Add(add1);
                sensorinfo.UserID = 1;
                New_Soy new_Soy = new New_Soy();
                new_Soy.new_SenID = sensorinfo.SenID;
                new_Soy.new_SenIP = sensorinfo.IP;
                new_Soy.new_SenZt = sensorinfo.SenZT;
                new_Soy.new_UserID = sensorinfo.UserID;
                new_Soy.new_SoYTime = DateTime.Now;
                new_Soy.new_Z = sensorinfo.ZID;
                if (ui.Trim() == "q")
                {
                    new_Soy.new_Z = null;
                }
                new_Soy.new_D = sensorinfo.DID;

                db.SensorInfo.Add(sensorinfo);
                db.New_Soy.Add(new_Soy);
                db.SaveChanges();


            }

            else
            {
                obj = new
                {
                    success = "false",
                    message = "ok"
                };
            }


            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        }

        public ActionResult EditSen(int? SenID, string DID, int SFType, string ZID, string SenZT, string IP, int UserID = 1)
        {
            var obj = new
            {
                success = "true",
                message = "ok"
            };
            List<SensorInfo> sen2 = (from d in db.SensorInfo where d.ZID.Trim() == ZID.Trim() && ZID.Trim() != "" select d).ToList();
            if(sen2.Count() >= 2)
            {
                obj = new
                {
                    success = "false",
                    message = "ok"
                };
                return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);
            }
            SensorInfo sen = db.SensorInfo.Find(SenID);
            sen.DID = DID.Trim();
            sen.UserID = UserID;
            sen.SFType = SFType;
            sen.ZID = ZID.Trim();
            sen.SenZT = SenZT.Trim();
            sen.IP = IP.Trim();
            db.SaveChanges();

            New_Soy newsoy = (from d in db.New_Soy where d.new_SenID == SenID select d).Single();
            newsoy.new_SenIP = IP.Trim();
            newsoy.new_SenZt = SenZT.Trim();
            newsoy.new_D = DID.Trim();
            newsoy.new_Z = ZID.Trim();
            newsoy.new_UserID = UserID;
            try
            {
                db.SaveChanges();
                return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);//}
            }
            catch (Exception)
            {
                obj = new
                {
                    success = "false",
                    message = "ok"
                };
                return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);

            }
            //List<SensorInfo> sen1 = (from d in db.SensorInfo where d.DID.Trim() == DID.Trim() || d.IP.Trim() == IP.Trim() || d.SenID == SenID select d).ToList();
            //if (sen1.Count() == 0)
            //{
            
            //else
            //{
            //obj = new
            //{
            //    success = "false",
            //    message = "ok"
            //};
            //}

            



        }
        public ActionResult EditSenez(int? SenID, string DID, int SFType, string ZID, string SenZT, string IP, int UserID = 1)
        {

            //List<SensorInfo> sen1 = (from d in db.SensorInfo where d.DID.Trim() == DID.Trim() || d.IP.Trim() == IP.Trim() || d.SenID == SenID select d).ToList();
            //if (sen1.Count() == 0)
            //{

            List<SensorInfo> sen2 = (from d in db.SensorInfo where d.ZID.Trim() == ZID.Trim() && ZID.Trim() != "" select d).ToList();
            for (int i = 0; i < sen2.Count; i++)
            {
                sen2[i].ZID = "";
            }
            SensorInfo sen = db.SensorInfo.Find(SenID);
            if (sen.SenZT.Trim() == "抑制")
            {
                SenZT = "正常";
            }
            else
            {
                SenZT = "抑制";

            }
            sen.DID = DID.Trim();
            sen.UserID = UserID;
            sen.SFType = SFType;
           // sen.ZID = "";
            sen.SenZT = SenZT.Trim();
            sen.IP = IP.Trim();
            db.SaveChanges();
            List<New_Soy> sen3 = (from d in db.New_Soy where d.new_Z.Trim() == ZID.Trim() && ZID.Trim() != "" select d).ToList();
            for (int i = 0; i < sen3.Count; i++)
            {
                sen3[i].new_Z = "";
            }
            New_Soy newsoy = (from d in db.New_Soy where d.new_SenID == SenID select d).Single();
            newsoy.new_SenIP = IP.Trim();
            newsoy.new_SenZt = SenZT.Trim();
            newsoy.new_D = DID.Trim();
            //newsoy.new_Z = "";
            newsoy.new_UserID = UserID;

            db.SaveChanges();
            //}
            var obj = new
            {
                success = "true",
                message = "ok"
            };
            //else
            //{
            //obj = new
            //{
            //    success = "false",
            //    message = "ok"
            //};
            //}

            return Json(obj, "text/plain", JsonRequestBehavior.AllowGet);



        }
        //public ActionResult DeleteS(List<int?> SenID)
        //{

        //    foreach (int i in SenID)
        //    {
        //        SensorInfo sensor = db.SensorInfo.Find(i);
        //        db.SensorInfo.Remove(sensor);
        //        //New_Soy new_Soy = (from d in db.New_Soy where d.new_SenID == i select d).Single();
        //        //db.New_Soy.Remove(new_Soy);
        //        ////New_Soy test1 = db.Customers.Single(c => c.CustomerID == 1);
        //        ////db.t_sysrole.DeleteOnSubmit(test1);
        //    }
        //    db.SaveChanges();
        //    return Content("success");
        //}
        public ActionResult DeleteS(List<int?> SenID)
        {
            foreach (int i in SenID)
            {
                SensorInfo user = db.SensorInfo.Find(i);
                db.SensorInfo.Remove(user);
                New_Soy new_Soy = (from d in db.New_Soy where d.new_SenID == i select d).Single();
                db.New_Soy.Remove(new_Soy);
                adress add = (from d in db.adress where d.SenID.Trim() == i + "A" select d).Single();
                db.adress.Remove(add);
                adress add1 = (from d in db.adress where d.SenID.Trim() == i + "B" select d).Single();
                db.adress.Remove(add1);
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
