using Microsoft.AspNetCore.Mvc;

namespace Roomies.Controllers
{
    [Route("app/[controller]")]
    public class AppController : Controller
    {
        [HttpGet]
        public IActionResult Get(){
            return Ok("This is the app controller");
        }
    }

}