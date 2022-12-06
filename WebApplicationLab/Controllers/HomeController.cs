using Microsoft.AspNetCore.Mvc;

namespace WebApplicationLab.Controllers
{

    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        static CounterModel model = new CounterModel();
        struct CounterModel
        {
            public int Counter;
        }

        

        [HttpGet]
        public IActionResult Get()
        {
            model.Counter += 1;
            return Ok(Json(model.Counter)); 
        }

        [HttpGet ("/Result")]
        public IActionResult GetResult()
        {
            return Ok(Json(model.Counter));
        }
    }
}
