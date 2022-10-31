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

        const string BGG_API_URL = "https://api.geekdo.com/xmlapi2/";

        public HomeController(IWebHostEnvironment _environment)
        {
            this.Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CollectionView()
        {
            List<Thing> things = new List<Thing>();

            // Load the document
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(BGG_API_URL, "collection?username=freelancermike"));

            //Loop through the selected Nodes using XPATH.
            foreach (XmlNode node in doc.SelectNodes("/items/item"))
            {
                //Fetch the Node values and assign it to Model
                things.Add(new Thing
                {
                    ThingId = int.Parse(node.Attributes["objectid"].InnerText),
                    Name = node["name"].InnerText,
                    YearPublished = node["yearpublished"].InnerText
                });
            }
            return View(things);
        }

        public IActionResult HotList()
        {
            List<Thing> things = new List<Thing>();

            // Load the document
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(BGG_API_URL, "hot"));

            //Loop through the selected Nodes.
            foreach (XmlNode node in doc.SelectNodes("/items/item"))
            {
                string year = "N/A";
                if (node["yearpublished"] != null)
                {
                    year = node["yearpublished"].GetAttribute("value");
                }
                //Fetch the Node values and assign it to Model
                things.Add(new Thing
                {
                    ThingId = int.Parse(node.Attributes["id"].InnerText),
                    Name = node["name"].GetAttribute("value"),
                    YearPublished = year
                });
            }
            return View(things);
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
