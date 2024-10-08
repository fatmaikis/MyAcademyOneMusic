﻿using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Areas.Artist.Models
{
    public class SongViewModel
    {
      
            public int SongID { get; set; }
            public string SongName { get; set; }
            public string SongUrl { get; set; }
            public IFormFile SongFile { get; set; }
            public int AlbumID { get; set; }
            public Album Album { get; set; }
        
    }
}
