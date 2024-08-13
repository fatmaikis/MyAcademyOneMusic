using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MySongController : Controller
    {
        private readonly ISongService _songService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;

        public MySongController(ISongService songService, UserManager<AppUser> userManager, IAlbumService albumService)
        {
            _songService = songService;
            _userManager = userManager;
            _albumService = albumService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _songService.TGetSongsWithAlbumByUserID(user.Id);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSong()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var albumList = _albumService.TGetAlbumsByArtist(user.Id);
            List<SelectListItem> albums = (from x in albumList
                                           select new SelectListItem
                                           {
                                               Text = x.AlbumName,
                                               Value = x.AlbumID.ToString()
                                           }).ToList();
            ViewBag.albums = albums;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong(SongViewModel model)
        {
            var song = new Song
            {
                SongName = model.SongName,
                AlbumID = model.AlbumID,
            };
            if (model.SongFile != null)
            {
                var resource = Directory.GetCurrentDirectory(); //Projenin bulunduğu şuanki dosya yolunu bul
                var extension = Path.GetExtension(model.SongFile.FileName).ToLower(); // dosyanın uzantısını bul
                if (extension != ".mp3")
                {
                    //desteklenmeyen dosya uzantısı hatası
                    ModelState.AddModelError("SongFile", "Sadece resim dosyaları kabul edilir.");
                    //gerekirse işlemi sonlandırabilirsiniz
                    return View(model);
                }
                var songName = Guid.NewGuid() + extension; // dosya adı
                var saveLocation = resource + "/wwwroot/songs/" + songName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await model.SongFile.CopyToAsync(stream);
                song.SongUrl = "/songs/" + songName;
            }
            _songService.TCreate(song);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSong(int id)
        {
            _songService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateSongArtist(int id)
        {
            var values = _songService.TGetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _albumService.TGetAlbumsByArtist(user.Id);

            List<SelectListItem> album = (from x in value
                                          select new SelectListItem
                                          {
                                              Text = x.AlbumName,
                                              Value = x.AlbumID.ToString()
                                          }).ToList();
            ViewBag.albumList = album;

            var model = new UpdateSongViewModel()
            {
                SongID = values.SongID,
                SongFileUrl = values.SongUrl,
                SongName = values.SongName,
                AlbumID = values.AlbumID

            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateSongArtist(UpdateSongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Validation failed; reload the page with the existing data
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var value = _albumService.TGetAlbumsByArtist(user.Id);

                List<SelectListItem> album = (from x in value
                                              select new SelectListItem
                                              {
                                                  Text = x.AlbumName,
                                                  Value = x.AlbumID.ToString()
                                              }).ToList();
                ViewBag.albumList = album;

                return View(model);
            }

            // Retrieve the existing song entity from the database
            var existingSong = _songService.TGetById(model.SongID);
            if (existingSong == null)
            {
                return NotFound();
            }

            // Update the existing song with the new values from the model
            existingSong.SongName = model.SongName;
            existingSong.SongUrl = model.SongFileUrl;
            existingSong.AlbumID = (int)model.AlbumID;

            // Save changes to the database
            _songService.TUpdate(existingSong);

            // Redirect to a different action after a successful update
            return RedirectToAction("Index"); // Redirect to an appropriate action or view
        }

    }
}
