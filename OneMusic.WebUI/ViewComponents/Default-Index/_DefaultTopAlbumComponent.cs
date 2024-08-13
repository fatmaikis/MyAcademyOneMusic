using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultTopAlbumComponent(IAlbumService _albumService) :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var values = _albumService.TGetAlbumsWithArtist().OrderByDescending(x => x.AlbumID).Take(6).ToList();
            //return View(values);
            var values = _albumService.TGetAlbumsWithArtist().ToList();
            var rnd = new Random();
            var rndList = values.OrderBy(x => rnd.Next()).Take(5).ToList(); 
            return View(rndList);
        }
    }
}

