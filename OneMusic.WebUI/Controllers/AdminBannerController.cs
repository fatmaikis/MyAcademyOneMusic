using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminBannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public AdminBannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public IActionResult Index()
        {
            var value = _bannerService.TGetList();
            return View(value);
        }

        public IActionResult CreateBanner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBanner(Banner banner)
        {
            _bannerService.TCreate(banner);
            return RedirectToAction("Index");   
        }
        public IActionResult DeleteBanner(int id)
        {
            _bannerService.TDelete(id);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateBanner(int id)
        {
           var value =  _bannerService.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateBanner(Banner banner)
        {
            _bannerService.TUpdate(banner);
            return RedirectToAction("Index");
        }
    }
}
