﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel model,string? returnUrl)
		{
			var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false,false );
			if (result.Succeeded) 
			{
				var user = await _userManager.FindByNameAsync(User.Identity.Name);
				var artistResult = await _userManager.IsInRoleAsync(user, "Artist");
				var adminResult = await _userManager.IsInRoleAsync(user, "Admin");

				if (artistResult==true)
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}


					return RedirectToAction("Index", "MyAlbum", new {area="Artist"});
				}
				else if (adminResult == true)
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}

					return RedirectToAction("Index", "AdminAbout");
				}
				else
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "Default");
				}

			}
			ModelState.AddModelError("","Kullanıcı adı veya şifre yanlış");
			return View();
		}

		public async Task<ActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Default");
		}



	}
}
