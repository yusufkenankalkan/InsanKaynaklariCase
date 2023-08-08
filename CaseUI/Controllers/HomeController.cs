using CaseDL;
using CaseEL.Models;
using CaseEL.ViewModels;
using CaseUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CaseUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSicil(SicilVM model)
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
    }
}