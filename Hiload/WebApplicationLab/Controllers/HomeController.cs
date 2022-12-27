using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using WebApplicationLab.RabbitMQ;

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
        private IRabbitMqService _mqService;
        static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(option);

        public HomeController(IRabbitMqService mqService)
        {
          _mqService = mqService;
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

    [Route("Send")]
    [HttpGet]
    public IActionResult SendMessage(string message)
    {
      _mqService.SendMessage(message + DateTime.Now.ToString());

      return Ok("Сообщение отправлено");
    }
  }
}
