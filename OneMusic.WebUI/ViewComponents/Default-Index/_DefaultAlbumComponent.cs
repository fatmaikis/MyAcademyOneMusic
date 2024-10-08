﻿using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultAlbumComponent(IAlbumService _albumService):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = _albumService.TGetAlbumsWithArtist();
            return View(values);
        }

        
    }
}
