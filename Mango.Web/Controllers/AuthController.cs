using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers;
[Route("auth")]
public class AuthController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
