using Microsoft.AspNetCore.Mvc;
using ASM.Areas.Admin.Controllers;

namespace ASM.Areas.Admin.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
