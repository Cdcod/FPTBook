using Microsoft.AspNetCore.Mvc;
using ASM.Controllers;
namespace ASM.ViewComponents
{
    public class HeaderSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
