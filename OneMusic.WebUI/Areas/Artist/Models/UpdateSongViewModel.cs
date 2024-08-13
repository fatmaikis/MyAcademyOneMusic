using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Areas.Artist.Models
{
    public class UpdateSongViewModel
    {
        public int SongID { get; set; }
        public string SongName { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile SongFile { get; set; }
        public string SongFileUrl { get; set; }
        public int? AlbumID { get; set; }
        public Album Album { get; set; }
    }
}
