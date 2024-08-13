using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultPopulerArtistComponent : ViewComponent
    {
        private readonly ISingerService _service;

        public _DefaultPopulerArtistComponent(ISingerService service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            var artist = _service.TGetList().OrderBy(x=>x.SingerID).Take(6).ToList();
            return View(artist);
        }
    }
}
