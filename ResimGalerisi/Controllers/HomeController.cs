using Microsoft.AspNetCore.Mvc;
using ResimGalerisi.Classes;
using ResimGalerisi.Models;
using System.Diagnostics;

namespace ResimGalerisi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UygulamaDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger,UygulamaDbContext db, IWebHostEnvironment env)
        {
            _logger = logger;
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            var vm = new ResimViewModel()
            {
                Resims = _db.resimler.ToList()
            };
            return View(vm);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Index(ResimViewModel vm)
        {
            if(ModelState.IsValid)
            {
                //guid kısmı dosya adını alır pathtten sonra olan kısım uzantısını alır ornk: .jpg 
                string yeniDosyaAd=Guid.NewGuid().ToString() +Path.GetExtension(vm.Dosya.FileName);
                string kayitYolu = Path.Combine(_env.WebRootPath, "img", yeniDosyaAd);
                using (var stream = new FileStream(kayitYolu,FileMode.Create))
                {
                    vm.Dosya.CopyTo(stream);   
                }
                _db.resimler.Add(new Resim()
                {
                    Baslik=vm.Baslik,
                    DosyaAd=yeniDosyaAd
                });
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            vm.Resims=_db.resimler.ToList();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}