using Microsoft.AspNetCore.Mvc;
using Ornk.Classes;
using Ornk.Models;
using System.Diagnostics;

namespace Ornk.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        public static HashSet<string> resimler = new HashSet<string>();
        public HomeController(ILogger<HomeController> logger,IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public IActionResult Index()
        {
              return View(resimler);
        }
        [HttpPost]
        public IActionResult Index(IFormFile? resim)
        {
            if (resim == null || resim.Length == 0)
                ModelState.AddModelError("resim", "Bir dosya Seçmediniz");
            else if (!resim.ContentType.StartsWith("image/"))
                ModelState.AddModelError("resim", "Geçersiz bir resim dosyası seçtiniz");
            if(resim != null)
            {
                string yol = Path.Combine(_env.WebRootPath, "img", resim!.FileName);
                resimler.Add(resim.FileName);
                using (var stream = new FileStream(yol, FileMode.OpenOrCreate))
                {
                    resim.CopyTo(stream);
                }
            }
            return View(resimler);
        }
        string[] YuklenenResimler()
        {
            return Directory
                .GetFiles(Path.Combine(_env.WebRootPath,"img"))
                .Select(x=>Path.GetFileName(x))
                .ToArray();
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