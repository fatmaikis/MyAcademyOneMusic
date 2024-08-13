using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultContactComponent(IContactService _contactService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var value = _contactService.TGetList();
            return View(value);
        } 
    }
}
