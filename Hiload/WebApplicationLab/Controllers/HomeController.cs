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
            var a = db.StringIncrement("model");
            var g = db.StringGet("model");
            return Ok(Json(g)); 
        }

        [HttpGet("/Result")]
        public IActionResult GetResult()
        {
          var db = _redis.GetDatabase();
          var g = db.StringGet("model");
          return Ok(Json(g));
        }
  }
}
