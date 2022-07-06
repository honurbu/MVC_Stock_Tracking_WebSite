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
    public class KategoriController : Controller
    {

        MvcDbStockEntities db = new MvcDbStockEntities();
        // GET: Kategori
        public ActionResult Index(string sf, int sayfa=1)
        {
            if(!string.IsNullOrEmpty(sf))
            {
                var sonuc = db.TBLKATEGORI.ToList().Where(m => m.KATEGORIAD.ToUpper().Contains(sf.ToUpper())).ToPagedList(sayfa, 5);
                return View(sonuc); 
            }

            var degerler = db.TBLKATEGORI.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }


        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORI p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }

            db.TBLKATEGORI.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var getir = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir", getir);
        }

       /* [HttpGet]
        public ActionResult Güncelle()
        {
            return View();  --> Bu eğer güncelle ekranı ile uğraşmak istemiyorsak direkt htmlcs
                                kodlarını güncelle içine yazar geçeriz dyte
        }
        [HttpPost]*/
        public ActionResult Güncelle(TBLKATEGORI p1)
        {

            var deger = db.TBLKATEGORI.Find(p1.KATEGORIID);
            deger.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}