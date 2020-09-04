using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DRIECT_Proxy.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DRIECT_Proxy.Controllers
{
    public class DIRECTController : Controller
    {
        private IWebHostEnvironment Environment;
        public DIRECTController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        [Route("/DIRECT/GET")]
        public async Task<IActionResult> GETAsync()
        {
                        

            string url = "";

            if (Request.QueryString.Value.Split("?url=")[1]!="")
                url = Request.QueryString.Value.Split("?url=")[1];


            url = System.Net.WebUtility.UrlDecode(url);
            List<string> good = new List<string>();
            List<string> gooduris = new List<string>();
            string s;
            s = await GetXML(url);


            XDocument x = XDocument.Parse(s);

            return new ContentResult
            {
                Content = x.ToString(),
                ContentType = "text/xml",
                StatusCode = 200
            };

        }
        [HttpGet]
        [ActionName("GET")]
        [Route("/DIRECT/GET/{url?}")]
        public async Task<IActionResult> GETAsync(string url)
        {
            url = System.Net.WebUtility.UrlDecode(url);
            List<string> good = new List<string>();
            List<string> gooduris = new List<string>();
            string s;
            s = await GetXML(url);


            XDocument x = XDocument.Parse(s);

            return new ContentResult
            {
                Content = x.ToString(),
                ContentType = "text/xml",
                StatusCode = 200
            };


        }

        private static async Task<string> GetXML(string uri)
        {
            try
            {

                StringContent queryString = new StringContent("nothing");

                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        var result = await client.GetStringAsync(uri);

                        if (!result.ToLower().Contains("<head"))
                            return result;
                        else
                        {

                            Console.WriteLine($"{uri}");
                            Console.WriteLine($"error: HTML data returned");
                            return "";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{uri}");
                Console.WriteLine($"error: {ex.Message}");
                return $"";
            }


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
