using Microsoft.AspNetCore.Mvc;

namespace NotificationProject.Controllers.SignalR
{
    public class ClientSideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
