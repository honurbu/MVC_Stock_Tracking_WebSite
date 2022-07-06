using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Stock.Models.Entity;

namespace MVC_Stock.Controllers
{
     public class SatisController : Controller
    {
        MvcDbStockEntities db = new MvcDbStockEntities();

        // GET: Satis
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR satis)
        {
            db.TBLSATISLAR.Add(satis);
            db.SaveChanges();
            return View("Index");
        }
    }
}