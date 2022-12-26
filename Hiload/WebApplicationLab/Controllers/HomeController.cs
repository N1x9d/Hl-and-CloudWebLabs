using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace WebApplicationLab.Controllers
{

    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        static ConfigurationOptions option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = { "localhost:6379" }
        };
        static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(option);

        public HomeController()
        {
            

        }

 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var db = _redis.GetDatabase();
            var a = db.StringIncrement("test");
            var g = db.StringGet("test");
            return Ok(Json(g)); 
        }

        //[HttpGet ("/Result")]
        //public IActionResult GetResult()
        //{
        //    return Ok(Json(model.Counter));
        //}
    }
}
