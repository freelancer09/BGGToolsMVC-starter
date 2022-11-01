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

        [HttpGet]
        public IActionResult CollectionView(string userName = null)
        {
            if (userName == null)
            {
                return View();
            }
            else
            {
                List<Thing> things = new List<Thing>();

                // Load the API XML document
                XmlDocument doc = new XmlDocument();
                doc.Load(string.Concat(BGG_API_URL, "collection?username=", userName));

                // If a collection is loaded, process the THINGS
                if (doc.DocumentElement.Name == "items")
                {
                    // Loop through the selected Nodes using XPATH
                    foreach (XmlNode node in doc.SelectNodes("/items/item"))
                    {
                        string year = "N/A";
                        if (node["yearpublished"] != null)
                            year = node["yearpublished"].InnerText;

                        // Fetch the Node values and assign it to Model
                        things.Add(new Thing
                        {
                            ThingId = int.Parse(node.Attributes["objectid"].InnerText),
                            Name = node["name"].InnerText,
                            YearPublished = year
                        });
                    }
                    ViewBag.UserName = userName;
                }

                // Check for errors and return the message
                else if (doc.DocumentElement.Name == "errors")
                {
                    string errorMessage = null;
                    foreach (XmlNode node in doc.SelectNodes("/errors/error"))
                    {
                        errorMessage += node["message"].InnerText + " ";
                    }
                    ViewBag.ErrorMessage = errorMessage;
                }

                // Check for errors and return the message
                else if (doc.DocumentElement.Name == "message")
                {
                    ViewBag.ErrorMessage = doc.DocumentElement.InnerText;
                }

                // Send list of THING to the view
                return View(things);
            }
        }

        public IActionResult HotList()
        {
            List<Thing> things = new List<Thing>();

            // Load the API XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(BGG_API_URL, "hot"));

            // Loop through the selected Nodes using XPATH
            foreach (XmlNode node in doc.SelectNodes("/items/item"))
            {
                string year = "N/A";
                if (node["yearpublished"] != null)
                    year = node["yearpublished"].GetAttribute("value");
                
                // Fetch the Node values and assign it to Model
                things.Add(new Thing
                {
                    ThingId = int.Parse(node.Attributes["id"].InnerText),
                    Name = node["name"].GetAttribute("value"),
                    YearPublished = year
                });
            }

            // Send List of THING to the view
            return View(things);
        }

        [Route("Home/Thing/{id}")]
        public IActionResult Thing(int id)
        {
            Thing thing = new Thing();

            // Load the API XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(BGG_API_URL, "thing?id=", id));

            // Loop through the selected Nodes using XPATH and assign to Model
            foreach (XmlNode node in doc.SelectNodes("items/item"))
            {
                thing.ThingId = int.Parse(node.Attributes["id"].InnerText);

                thing.Name = node["name"].GetAttribute("value");

                thing.Description = node["description"].InnerText;

                string year = "N/A";
                if (node["yearpublished"] != null)
                    year = node["yearpublished"].GetAttribute("value");
                thing.YearPublished = year;

                thing.Thumbnail = node["thumbnail"].InnerText;

                thing.Image = node["image"].InnerText;

                thing.MinPlayers = int.Parse(node["minplayers"].GetAttribute("value"));

                thing.MaxPlayers = int.Parse(node["maxplayers"].GetAttribute("value"));

                thing.MinPlayTime = int.Parse(node["minplaytime"].GetAttribute("value"));

                thing.MaxPlayTime = int.Parse(node["maxplaytime"].GetAttribute("value"));

                thing.MinAge = int.Parse(node["minage"].GetAttribute("value"));
            }

            // Send THING to the view
            return View(thing);
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
