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
using Newtonsoft.Json;

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


        public async Task<IActionResult> GetUnit(int id=0)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync(apiurl + "/apigw/unit/" + id.ToString());

            //ViewBag.Json = JArray.Parse(content).ToString();
            return Json(content);
        }

        public async Task<IActionResult> DelUnit(int id =0)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsync(apiurl + "/apigw/unit/del/" + id.ToString(), null);

            //ViewBag.Json = JArray.Parse(content.Content.ToString()).ToString();

            var result = await response.Content.ReadAsStringAsync();

            return Json(result);


            
        }

        public async Task<IActionResult> AddUnit()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var jdata = new { id = 0, code = "code5", isActive = true, name = "XX" + DateTime.Now.ToShortTimeString(), created_at = DateTime.Now, created_by = 1 };
            //var payload = "{ \"id\":0, \"code\": \"code2\",\"isActive\": true,\"name\":\"XXX\", \"created_at\":\"2022-02-19T06:32:08.082Z\",\"created_by\":1 }";

            string jsonstring = JsonConvert.SerializeObject(jdata);
            

            HttpContent c = new StringContent(jsonstring.ToString(), Encoding.UTF8, "application/json");
            c.Headers.ContentType= new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiurl + "/apigw/unit/add", c);

            var result = await  response.Content.ReadAsStringAsync();

            return Json(result);
        }

        public async Task<IActionResult> UpdateUnit()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var jdata = new { id = 1, code = "code5", isActive = true, name = "XX" + DateTime.Now.ToShortTimeString(), created_at = DateTime.Now, created_by = 1 };
            //var payload = "{ \"id\":0, \"code\": \"code2\",\"isActive\": true,\"name\":\"XXX\", \"created_at\":\"2022-02-19T06:32:08.082Z\",\"created_by\":1 }";

            string jsonstring = JsonConvert.SerializeObject(jdata);


            HttpContent c = new StringContent(jsonstring.ToString(), Encoding.UTF8, "application/json");
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiurl + "/apigw/unit/update", c);

            var result = await response.Content.ReadAsStringAsync();

            return Json(result);

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