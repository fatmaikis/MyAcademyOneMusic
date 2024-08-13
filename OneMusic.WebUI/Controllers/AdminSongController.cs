using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminSongController : Controller
    {
        private readonly ISongService _songService;

        public AdminSongController(ISongService songService)
        {
            _songService = songService;
        }

        public IActionResult Index()
        {
            var value = _songService.TGetList();
            return View(value);
        }

        public IActionResult CreateSong()
        {
            //var songList = _songService.TGetList();
            //List<SelectListItem> song = (from x in songList
            //                             select new SelectListItem
            //                             {
            //                                 Text = x.Album.AlbumName,
            //                                 Value = x.Album.AlbumName

            //                             })
            return View();
        }
        [HttpPost]
        public IActionResult CreateSong(Song song)
        {
            _songService.TCreate(song);
            return RedirectToAction("Index");

        }
        public IActionResult DeleteSong(int id)
        {
            _songService.TDelete(id);
            return RedirectToAction("Index");
        }
        public IActionResult UpdateSong(int id)
        {
            var value = _songService.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateSong(Song song)
        {
            _songService.TUpdate(song);
            return RedirectToAction("Index");
        }
    }
}
