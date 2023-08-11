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
        private readonly ISicilOgrenimManager _sicilOgrenimManager;
        private readonly IAdayCvManager _adayCvManager;

        public HomeController(MyContext context, ISicilManager sicilManager, ISicilUcretManager sicilUcretManager, ISicilOgrenimManager sicilOgrenimManager, IAdayCvManager adayCvManager)
        {
            _context = context;
            _sicilManager = sicilManager;
            _sicilUcretManager = sicilUcretManager;
            _sicilOgrenimManager = sicilOgrenimManager;
            _adayCvManager = adayCvManager;
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
            var sicilListesi = _sicilManager.GetAll(s => s.AktifMi == true).Data;
            foreach (var sicil in sicilListesi)
            {
                if (_sicilUcretManager.GetByConditions(s => s.SicilNo == sicil.SicilNo).IsSuccess)
                    sicil.SicilUcret = _sicilUcretManager.GetByConditions(s => s.SicilNo == sicil.SicilNo).Data;
            }

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

            SicilUcretVM newUcret = new SicilUcretVM
            {
                SicilNo = model.SicilNo,
                UcretTipi = model.UcretTipi,
                UcretTutari = model.UcretTutari,
                GecerlilikBaslangicTarihi = model.GecerlilikBaslangicTarihi
            };

            _sicilUcretManager.Add(newUcret);

            return RedirectToAction("SicilUcret");

        }

        [HttpGet]
        public IActionResult UcretDuzenle(int id)
        {
            var sicilUcret = _sicilUcretManager.GetByConditions(s => s.SicilNo == id).Data;

            if (sicilUcret == null)
            {
                return NotFound();
            }

            var sicil = _sicilManager.GetByConditions(s => s.SicilNo == id).Data;

            if (sicil == null)
            {
                return NotFound();
            }

            var model = new SicilUcretVM
            {
                SicilNo = id,
                UcretTipi = sicilUcret.UcretTipi,
                UcretTutari = sicilUcret.UcretTutari,
                GecerlilikBaslangicTarihi = sicilUcret.GecerlilikBaslangicTarihi
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult UcretDuzenle(int id, SicilUcretVM model)
        {

            var sicilUcret = _context.SicilUcret.SingleOrDefault(s => s.SicilNo == id);

            if (sicilUcret == null)
            {
                return NotFound();
            }

            sicilUcret.SicilNo = model.SicilNo;
            sicilUcret.UcretTipi = model.UcretTipi;
            sicilUcret.UcretTutari = model.UcretTutari;
            sicilUcret.GecerlilikBaslangicTarihi = model.GecerlilikBaslangicTarihi;


            _context.SaveChanges();

            return RedirectToAction("SicilUcret");

        }

        [HttpGet]
        public IActionResult SicilOgrenim()
        {
            var sicilListesi = _sicilManager.GetAll(s => s.AktifMi == true).Data;
            foreach (var sicil in sicilListesi)
            {
                if (_sicilOgrenimManager.GetByConditions(s => s.SicilNo == sicil.SicilNo).IsSuccess)
                    sicil.SicilOgrenim = _sicilOgrenimManager.GetByConditions(s => s.SicilNo == sicil.SicilNo).Data;
            }

            ViewBag.SicilListesi = sicilListesi;

            return View();
        }

        [HttpPost]
        public IActionResult OgrenimKaydet(SicilOgrenimVM model)
        {
            var relatedSicil = _sicilManager.GetByConditions(s => s.SicilNo == model.SicilNo).Data;


            if (relatedSicil == null)
            {
                ModelState.AddModelError("", "Sicil bulunamadı!");
                ViewBag.SicilListesi = _context.Sicil.Where(s => s.AktifMi == true).ToList();
                return View("SicilOgrenim", model);
            }

            model.Sicil = relatedSicil;

            SicilOgrenimVM newOgrenim = new SicilOgrenimVM
            {
                SicilNo = model.SicilNo,
                OgrenimBaslangicTarihi = model.OgrenimBaslangicTarihi,
                OgrenimBitisTarihi = model.OgrenimBitisTarihi,
                OgrenimDurumu = model.OgrenimDurumu,
                OkulAdi = model.OkulAdi,
            };

            _sicilOgrenimManager.Add(newOgrenim);

            return RedirectToAction("SicilOgrenim");
        }

        [HttpGet]
        public IActionResult OgrenimDuzenle(int id)
        {
            var sicilOgrenim = _sicilOgrenimManager.GetByConditions(s => s.SicilNo == id).Data;

            if (sicilOgrenim == null)
            {
                return NotFound();
            }

            var sicil = _sicilManager.GetByConditions(s => s.SicilNo == id).Data;

            if (sicil == null)
            {
                return NotFound();
            }

            var model = new SicilOgrenimVM
            {
                SicilNo = id,
                OgrenimDurumu = sicilOgrenim.OgrenimDurumu,
                OkulAdi = sicilOgrenim.OkulAdi,
                OgrenimBaslangicTarihi = sicilOgrenim.OgrenimBaslangicTarihi,
                OgrenimBitisTarihi = sicilOgrenim.OgrenimBitisTarihi
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult OgrenimDuzenle(int id, SicilOgrenimVM model)
        {
            var sicilOgrenim = _context.SicilOgrenim.SingleOrDefault(s => s.SicilNo == id);

            if (sicilOgrenim == null)
            {
                return NotFound();
            }

            sicilOgrenim.OgrenimDurumu = model.OgrenimDurumu;
            sicilOgrenim.OkulAdi = model.OkulAdi;
            sicilOgrenim.OgrenimBaslangicTarihi = model.OgrenimBaslangicTarihi;
            sicilOgrenim.OgrenimBitisTarihi = model.OgrenimBitisTarihi;

            _context.SaveChanges();

            return RedirectToAction("SicilOgrenim");
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
                    IseAlindiMi = false
                };

                _context.AdayCv.Add(newAday);
                _context.SaveChanges();

                return RedirectToAction("AdayCv");
            }


            return View(model);
        }

        [HttpPost]
        public IActionResult CvIseAl(AdayCvVM model)
        {
            var adayCv = _adayCvManager.GetByConditions(s => s.CvNo == model.CvNo).Data;

            SicilVM sicil = new SicilVM
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                BaslamaTarihi = DateTime.Now,
                DogumTarihi = model.DogumTarihi,
                AktifMi = true
            };
            _sicilManager.Add(sicil);

            SicilOgrenimVM newOgrenim = new SicilOgrenimVM
            {
                SicilNo = _sicilManager.GetByConditions(s => s.Ad == sicil.Ad && s.Soyad == sicil.Soyad && s.DogumTarihi == sicil.DogumTarihi).Data.SicilNo,
                OgrenimBaslangicTarihi = model.OgrenimBaslangicTarihi,
                OgrenimBitisTarihi = model.OgrenimBitisTarihi,
                OgrenimDurumu = model.OgrenimDurumu,
                OkulAdi = model.OkulAdi,
            };

            _sicilOgrenimManager.Add(newOgrenim);
            adayCv.IseAlindiMi = true;
            _adayCvManager.Update(adayCv);

            return RedirectToAction("AdayCv");
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
            var aktifCalisanlar = _sicilManager.GetAll(x => x.AktifMi == true).Data;

            var sicilOgrenimBilgileri = _context.SicilOgrenim.ToList();

            var raporListesi = new List<SicilRaporVM>();

            foreach (var sicil in aktifCalisanlar)
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
            return Json(raporListesi);
        }

        [HttpGet]
        public IActionResult GetPasifCalisanlar()
        {
            var pasifCalisanlar = _sicilManager.GetAll(x => x.AktifMi == false).Data;

            var sicilOgrenimBilgileri = _context.SicilOgrenim.ToList();

            var raporListesi = new List<SicilRaporVM>();

            foreach (var sicil in pasifCalisanlar)
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
            return Json(raporListesi);
        }

        [HttpGet]
        public IActionResult GetTumCalisanlar()
        {
            var tumCalisanlar = _sicilManager.GetAll().Data;

            var sicilOgrenimBilgileri = _context.SicilOgrenim.ToList();

            var raporListesi = new List<SicilRaporVM>();

            foreach (var sicil in tumCalisanlar)
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
            return Json(raporListesi);
        }


    }
}