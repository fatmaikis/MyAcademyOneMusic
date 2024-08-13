using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new ArtistEditViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ArtistEditViewModel model)
        {
            ModelState.Clear();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.ImageFile != null)
            {
                var resource = Directory.GetCurrentDirectory(); //Projenin bulunduğu şuanki dosya yolunu bul
                var extension = Path.GetExtension(model.ImageFile.FileName).ToLower(); // dosyanın uzantısını bul
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    //desteklenmeyen dosya uzantısı hatası
                    ModelState.AddModelError("ImageFile","Sadece resim dosyaları kabul edilir.");
                    //gerekirse işlemi sonlandırabilirsiniz
                    return View(model);
                }
                var imageName = Guid.NewGuid() + extension; // dosya adı
                var saveLocation = resource + "/wwwroot/images/" + imageName; //kaydedilecek yer
                var stream = new FileStream(saveLocation, FileMode.Create); //dosya kaydetme işlemleri yapılacak
                await model.ImageFile.CopyToAsync(stream); //oluşturdugumuz image klasörüne kopyalayacak
                user.ImageUrl = "/images/" + imageName; //veritabanına sadece yolunu kaydediyoruz 
            }

            
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await _userManager.CheckPasswordAsync(user,model.OldPassword);
            if (result==true)
            {
                if (model.NewPassword != null && model.ConfirmPassword == model.NewPassword)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var item in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            return View();
                        }
                    }

                }

                var uptadeResult =  await _userManager.UpdateAsync(user);
                if (uptadeResult.Succeeded)
                {
                    return RedirectToAction("Index","Login");
                }
            }
            ModelState.AddModelError("","Mevcut şifreniz hatalı");
            return View();
        }


    }
}
