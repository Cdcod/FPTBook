using Microsoft.AspNetCore.Mvc;
using ASM.Areas.Admin.Controllers;

namespace ASM.Areas.Admin.ViewComponents
{
    public class SliderViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
