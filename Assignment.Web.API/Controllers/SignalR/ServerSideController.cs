using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationProject.Hubs;
using Assignment.Web.API.Models;

namespace NotificationProject.Controllers.SignalR
{
    public class ServerSideController : Controller
    {
        private readonly IHubContext<Notification> _notification;

        public ServerSideController(IHubContext<Notification> notification)
        {
            _notification = notification;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(String title, String body, String bigImage, String largeImage)
        {
            List<String> list = new List<String> { title, body, bigImage, largeImage };

            await _notification.Clients.All.SendAsync("ReceiveNotification",list);
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(String mssg)
        //{
        //    await _notification.Clients.All.SendAsync("ReceiveMessage", mssg);
        //    return View();
        //}
    }
}
