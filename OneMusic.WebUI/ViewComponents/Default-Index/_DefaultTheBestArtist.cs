using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using System.ComponentModel;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultTheBestArtist :ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultTheBestArtist(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var value = _songService.TGetList().OrderByDescending(x=>x.SongID).Take(1).ToList();
            return View(value);
        }
    }
}
