using BGGToolsMvC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic;

namespace BGGToolsMvC.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment Environment;

        public HomeController(IWebHostEnvironment _environment)
        {
            this.Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CollectionView()
        {
            return View();
        }

        public IActionResult HotList()
        {
            return View();
        }

        public IActionResult Thing()
        {
            return View();
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
