using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultNewHitsSongs:ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultNewHitsSongs(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
           var values = _songService.TGetSongsWithAlbumAndArtist().OrderByDescending(x => x.SongID).Take(5).ToList();
            return View(values);
        }
    }
}
