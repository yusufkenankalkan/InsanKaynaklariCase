using CaseBL.ImplementationsOfManager;
using CaseBL.InterfacesOfManager;
using CaseDL;
using CaseEL.Models;
using CaseEL.ViewModels;
using CaseUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace CaseUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        private readonly ISicilManager _sicilManager;
        private readonly ISicilUcretManager _sicilUcretManager;

        

        public HomeController(MyContext context, ISicilManager sicilManager, ISicilUcretManager sicilUcretManager)
        {
            _context = context;
            _sicilManager = sicilManager;
            _sicilUcretManager = sicilUcretManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sicilListesi = _context.Sicil.Where(s => s.AktifMi == true).ToList();

            ViewBag.SicilListesi = sicilListesi;

            return View();
        }

        [HttpPost]
        public IActionResult SicilEkle(SicilVM model)
        {
            if (ModelState.IsValid)
            {

                Sicil newSicil = new Sicil
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    BaslamaTarihi = model.BaslamaTarihi,
                    BitisTarihi = model.BitisTarihi,
                    DogumTarihi = model.DogumTarihi,
                    AktifMi = model.AktifMi
                };

                _context.Sicil.Add(newSicil);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(model);
        }

        [HttpGet]
        public IActionResult SicilDuzenle(int id)
        {
            var sicil = _context.Sicil.Find(id);
            if (sicil == null)
            {
                return NotFound();
            }

            var model = new SicilVM
            {
                Ad = sicil.Ad,
                Soyad = sicil.Soyad,
                BaslamaTarihi = sicil.BaslamaTarihi,
                BitisTarihi = sicil.BitisTarihi,
                DogumTarihi = sicil.DogumTarihi,
                AktifMi = sicil.AktifMi
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SicilDuzenle(int id, SicilVM model)
        {
            if (ModelState.IsValid)
            {
                var sicil = _context.Sicil.SingleOrDefault(s => s.SicilNo == id);

                if (sicil == null)
                {
                    return NotFound();
                }

                sicil.Ad = model.Ad;
                sicil.Soyad = model.Soyad;
                sicil.BaslamaTarihi = model.BaslamaTarihi;
                sicil.BitisTarihi = model.BitisTarihi;
                sicil.DogumTarihi = model.DogumTarihi;
                sicil.AktifMi = model.AktifMi;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Sil(int id)
        {
            var sicil = _context.Sicil.Find(id);
            if (sicil == null)
            {
                return NotFound();
            }

            sicil.AktifMi = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SicilUcret()
        {
            var sicilListesi = _context.Sicil.Where(s => s.AktifMi == true).ToList();

            ViewBag.SicilListesi = sicilListesi;

            return View();
        }

        [HttpPost]
        public IActionResult UcretKaydet(SicilUcretVM model)
        {

            var relatedSicil = _sicilManager.GetByConditions(s => s.SicilNo == model.SicilNo).Data;


            if (relatedSicil == null)
            {
                ModelState.AddModelError("", "Sicil bulunamadı!");
                ViewBag.SicilListesi = _context.Sicil.Where(s => s.AktifMi == true).ToList();
                return View("SicilUcret", model);
            }

            model.Sicil = relatedSicil;


            if (ModelState.IsValid)
            {

                SicilUcret newUcret = new SicilUcret
                {
                    SicilNo = relatedSicil.SicilNo,
                    UcretTipi = model.UcretTipi,
                    UcretTutari = model.UcretTutari,
                    GecerlilikBaslangicTarihi = model.GecerlilikBaslangicTarihi
                };


                _context.SicilUcret.Add(newUcret);
                _context.SaveChanges();

                return RedirectToAction("SicilUcret");
            }


            ViewBag.SicilListesi = _context.Sicil.Where(s => s.AktifMi == true).ToList();
            return View("SicilUcret", model);
        }

        [HttpGet]
        public IActionResult SicilOgrenim()
        {
            var sicilListesi = _context.Sicil.Where(s => s.AktifMi == true).ToList();

            ViewBag.SicilListesi = sicilListesi;

            return View();
        }

        [HttpGet]
        public IActionResult AdayCv()
        {
            ViewBag.AdayCvListesi = _context.AdayCv.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AdayCvKaydet(AdayCvVM model)
        {
            if (ModelState.IsValid)
            {

                AdayCv newAday = new AdayCv
                {
                    CvNo = model.CvNo,
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    DogumTarihi = model.DogumTarihi,
                    CvOlusturmaTarihi = model.CvOlusturmaTarihi,
                    OgrenimDurumu = model.OgrenimDurumu,
                    OkulAdi = model.OkulAdi,
                    Bolum = model.Bolum,
                    OgrenimBaslangicTarihi = model.OgrenimBaslangicTarihi,
                    OgrenimBitisTarihi = model.OgrenimBitisTarihi,
                    IsYeriAdi = model.IsYeriAdi,
                    IsDetayi = model.IsDetayi,
                };

                _context.AdayCv.Add(newAday);
                _context.SaveChanges();

                return RedirectToAction("AdayCv");
            }


            return View(model);
        }

        [HttpGet]
        public IActionResult CvDuzenle(int id)
        {
            var aday = _context.AdayCv.Find(id);
            if (aday == null)
            {
                return NotFound();
            }

            var model = new AdayCvVM
            {
                Ad = aday.Ad,
                Soyad = aday.Soyad,
                DogumTarihi = aday.DogumTarihi,
                CvOlusturmaTarihi = aday.CvOlusturmaTarihi,
                OgrenimDurumu = aday.OgrenimDurumu,
                OkulAdi = aday.OkulAdi,
                Bolum = aday.Bolum,
                OgrenimBaslangicTarihi = aday.OgrenimBaslangicTarihi,
                OgrenimBitisTarihi = aday.OgrenimBitisTarihi,
                IsYeriAdi = aday.IsYeriAdi,
                IsDetayi = aday.IsDetayi
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CvDuzenle(int id, AdayCvVM model)
        {
            if (ModelState.IsValid)
            {
                var aday = _context.AdayCv.SingleOrDefault(s => s.CvNo == id);

                if (aday == null)
                {
                    return NotFound();
                }

                aday.Ad = model.Ad;
                aday.Soyad = model.Soyad;                
                aday.DogumTarihi = model.DogumTarihi;
                aday.CvOlusturmaTarihi = model.CvOlusturmaTarihi;
                aday.OgrenimDurumu = model.OgrenimDurumu;
                aday.OkulAdi = model.OkulAdi;
                aday.Bolum = model.Bolum;
                aday.OgrenimBaslangicTarihi = model.OgrenimBaslangicTarihi;
                aday.OgrenimBitisTarihi = model.OgrenimBitisTarihi;
                aday.IsYeriAdi = model.IsYeriAdi;
                aday.IsDetayi = model.IsDetayi;
                
                _context.SaveChanges();

                return RedirectToAction("AdayCv");
            }

            return View(model);
        }

        public IActionResult CvSil(int id)
        {
            var adayCv = _context.AdayCv.Find(id);
           
            if (adayCv == null)
            {
                return NotFound();
            }

            _context.AdayCv.Remove(adayCv);
            _context.SaveChanges();

            return RedirectToAction("AdayCv");
        }

        public IActionResult Rapor()
        {
            var tumSiciller = _context.Sicil.ToList();

            var sicilOgrenimBilgileri = _context.SicilOgrenim.ToList();

            var raporListesi = new List<SicilRaporVM>();

            foreach (var sicil in tumSiciller)
            {
                var ogrenimBilgisi = sicilOgrenimBilgileri.FirstOrDefault(o => o.SicilNo == sicil.SicilNo);

                var rapor = new SicilRaporVM
                {
                    SicilNo = sicil.SicilNo,
                    Ad = sicil.Ad,
                    Soyad = sicil.Soyad,
                    BaslamaTarihi = sicil.BaslamaTarihi,
                    BitisTarihi = sicil.BitisTarihi,
                    AktifMi = sicil.AktifMi,
                    OgrenimDurumu = ogrenimBilgisi?.OgrenimDurumu
                };

                raporListesi.Add(rapor);
            }

            ViewBag.RaporListesi = raporListesi;

            return View();
        }

        [HttpGet]
        public IActionResult GetAktifCalisanlar()
        {
            var aktifCalisanlar = _context.Sicil.Where(s => s.AktifMi).ToList();
            return Json(aktifCalisanlar);
        }

        [HttpGet]
        public IActionResult GetPasifCalisanlar()
        {
            var pasifCalisanlar = _context.Sicil.Where(s => !s.AktifMi).ToList();
            return Json(pasifCalisanlar);
        }

        [HttpGet]
        public IActionResult GetTumCalisanlar()
        {
            var tumCalisanlar = _context.Sicil.ToList();
            return Json(tumCalisanlar);
        }


    }
}