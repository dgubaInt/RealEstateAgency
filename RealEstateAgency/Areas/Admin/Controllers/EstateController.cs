using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class EstateController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
