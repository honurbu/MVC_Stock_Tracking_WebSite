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
    public class UrunController : Controller
    {
        MvcDbStockEntities db = new MvcDbStockEntities();

        // GET: Urun
        public ActionResult Index(string p, int page=1)
        {
            if(!string.IsNullOrEmpty(p))
            {
                var sonuc = db.TBLURUNLER.ToList().Where(m => m.URUNAD.ToUpper().Contains(p.ToUpper())).ToPagedList(page,5);
                return View(sonuc);

            }
            var degerler = db.TBLURUNLER.ToList().ToPagedList(page, 5);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();

            ViewBag.Degerler = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER u1)
        {
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == u1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            u1.TBLKATEGORI = ktg;
            db.TBLURUNLER.Add(u1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var degerler = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(degerler);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();

            ViewBag.Degerler = degerler;
            return View("UrunGetir", urun);
        }

        public ActionResult Güncelle(TBLURUNLER u2)
        {
            var urun = db.TBLURUNLER.Find(u2.URUNID);
            urun.URUNAD = u2.URUNAD;
            urun.MARKA = u2.MARKA;
            urun.FIYAT = u2.FIYAT;
            urun.STOK = u2.STOK;
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == u2.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}