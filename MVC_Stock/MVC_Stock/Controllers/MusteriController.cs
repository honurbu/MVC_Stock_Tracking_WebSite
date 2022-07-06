using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Stock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVC_Stock.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStockEntities db = new MvcDbStockEntities(); 

        // GET: Musteri
        public ActionResult Index(string p, int sf=1)
        {
            if (!string.IsNullOrEmpty(p))
            {
                var sonuc = db.TBLMUSTERILER.ToList().Where(m => m.MUSTERIAD.ToUpper().Contains(p.ToUpper())).ToPagedList(sf,5);
                return View(sonuc);
            }

            var degerler = db.TBLMUSTERILER.ToList().ToPagedList(sf, 5);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER m1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }

            db.TBLMUSTERILER.Add(m1);
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult SIL(int id)
        {
            var musteriSil = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteriSil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult MusteriGetir(int id)
        {
            var musteriGetir = db.TBLMUSTERILER.Find(id);
            return View("musteriGetir", musteriGetir);
        }

        public  ActionResult Güncelle(TBLMUSTERILER m1)
        {            
            var guncelle = db.TBLMUSTERILER.Find(m1.MUSTERID);
            guncelle.MUSTERIAD = m1.MUSTERIAD;
            guncelle.MUSTERISOYAD = m1.MUSTERISOYAD;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}