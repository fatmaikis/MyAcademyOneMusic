using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultBuyAlbumComponent:ViewComponent
    {
        private readonly IAlbumService _albumService;

        public _DefaultBuyAlbumComponent(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _albumService.TGetList().OrderByDescending(x => x.CategoryID).Take(7).ToList();
            return View(values);
        }
         
    }
}
