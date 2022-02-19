using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string apiurl = "https://localhost:44301";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

      
        public async Task<IActionResult> GetUnit()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync(apiurl + "/apigw/unit/1");

            //ViewBag.Json = JArray.Parse(content).ToString();
            return Json(content);
        }

        public async Task<IActionResult> DelUnit()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.PostAsync(apiurl + "/apigw/unit/del/1",null);

            //ViewBag.Json = JArray.Parse(content.Content.ToString()).ToString();
            return Json(content);
        }

        public async Task<IActionResult> AddUnit()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var jdata = new { code = 1, isActive = true, name = "XX" + DateTime.Now.ToShortTimeString(), created_at = DateTime.Now, created_by = 1 };

            string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(jdata); //"{ Code:\"1\", Name:\"dian\", created_at:\"2022-02-19T02: 11:03.916Z\", created_by=1 }";

            

            var stringContent = new StringContent( jsondata, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

            stringContent.Headers.Remove("Content-Type"); // "{application/json; charset=utf-8}"
            stringContent.Headers.Add("Content-Type", "application/json");


            var content = await client.PostAsync(apiurl + "/apigw/unit/add", stringContent);
            //var content = await client.PostAsJsonAsync(apiurl + "/apigw/unit/add", 
            //    new { code = 1, isactive=true, name="XX"+DateTime.Now.ToShortTimeString(), created_at=DateTime.Now, created_by=1 });

            ViewBag.Json = JArray.Parse(content.Content.ToString()).ToString();
            return View("json");
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}