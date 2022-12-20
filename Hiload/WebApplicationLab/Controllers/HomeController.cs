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
        static CounterModel model = new CounterModel();
        static ConfigurationOptions option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = { $"127.0.0.1:6379" }
        };
        static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(option);

        public HomeController()
        {
            

        }
        struct CounterModel
        {
            public int Counter;
        }

 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var db = _redis.GetDatabase();
            var s = JsonConvert.SerializeObject(model);
            var a = await db.SetAddAsync("model", s);
            var d = _redis.GetServer("host").Keys();
            //model.Counter =  Convert.ToInt32(db.StringGet("val"));
            model.Counter += 1;
            //db.SetAdd("val", model.Counter);
            return Ok(Json(model.Counter)); 
        }

        [HttpGet ("/Result")]
        public IActionResult GetResult()
        {
            return Ok(Json(model.Counter));
        }
    }
}
