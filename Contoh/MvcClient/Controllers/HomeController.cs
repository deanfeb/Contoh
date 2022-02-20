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
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

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

        public async Task<object> GetList(DataSourceLoadOptions loadOptions)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonstring = JsonConvert.SerializeObject(loadOptions);


            HttpContent c = new StringContent(jsonstring.ToString(), Encoding.UTF8, "application/json");
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiurl + "/apigw/unit/getlist2", c);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<IActionResult> GetUnit(int id = 0)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync(apiurl + "/apigw/unit/" + id.ToString());

            //ViewBag.Json = JArray.Parse(content).ToString();
            return Json(content);
        }

        public async Task<IActionResult> DelUnit(int key = 0)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsync(apiurl + "/apigw/unit/del/" + key.ToString(), null);

            //ViewBag.Json = JArray.Parse(content.Content.ToString()).ToString();

            var result = await response.Content.ReadAsStringAsync();

            return Json(result);



        }

        public async Task<IActionResult> SaveAdd(string values)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            Domain.Entities.Unit unit = new Domain.Entities.Unit();
            JsonConvert.PopulateObject(values, unit);
            unit.Created_at = DateTime.Now;
            //var jdata = new { id = 0, code = "code5", isActive = true, name = "XX" + DateTime.Now.ToShortTimeString(), created_at = DateTime.Now, created_by = 1 };
            //var payload = "{ \"id\":0, \"code\": \"code2\",\"isActive\": true,\"name\":\"XXX\", \"created_at\":\"2022-02-19T06:32:08.082Z\",\"created_by\":1 }";

            string jsonstring = JsonConvert.SerializeObject(unit);


            HttpContent c = new StringContent(jsonstring.ToString(), Encoding.UTF8, "application/json");
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiurl + "/apigw/unit/add", c);

            var result = await response.Content.ReadAsStringAsync();

            return Json(result);
        }

        public async Task<IActionResult> SaveUpdate(string values, int key)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringdata = await client.GetStringAsync(apiurl + "/apigw/unit/" + key.ToString());

            Domain.Entities.ResponseBase<Domain.Entities.Unit> data = JsonConvert.DeserializeObject<Domain.Entities.ResponseBase<Domain.Entities.Unit>>(stringdata);

            stringdata = JsonConvert.SerializeObject(data.Data);

            var updObj = new Domain.Entities.Unit();
            JsonConvert.PopulateObject(stringdata, updObj);
            JsonConvert.PopulateObject(values, updObj);
            


            string jsonstring = JsonConvert.SerializeObject(updObj);


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