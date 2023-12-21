using Microsoft.AspNetCore.Mvc;

namespace FPTBook.ViewComponents
{
    public class HumbergerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
