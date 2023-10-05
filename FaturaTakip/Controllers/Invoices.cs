using Microsoft.AspNetCore.Mvc;

namespace FaturaTakip.Controllers
{
    public class Invoices : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Invoices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            string activeUserEmail = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserEmail"];
            string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
            string activeUserIdStr = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserId"];
            if (activeUserToken == null) { RedirectToAction("Login", "Account"); }
            return View();

        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
