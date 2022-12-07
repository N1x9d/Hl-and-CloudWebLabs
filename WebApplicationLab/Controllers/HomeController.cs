using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;

namespace WebApplicationLab.Controllers
{

    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        static CounterModel model = new CounterModel();
        static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect($"localhost:2288,password=qwerty");

        public HomeController()
        {
            var redis = RedisStore.RedisCache;
        }
        struct CounterModel
        {
            public int Counter;
        }

 
        [HttpGet]
        public IActionResult Get()
        {
            var redis = RedisStore.RedisCache;
            var a = redis.GetType();
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
