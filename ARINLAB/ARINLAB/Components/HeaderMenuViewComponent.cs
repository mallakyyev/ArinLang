using ARINLAB.Services.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace TSTB.Web.Components
{
    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuServece;
        private readonly IMemoryCache _cache;

        public HeaderMenuViewComponent(IMenuService menuService, IMemoryCache cache)
        {
            _menuServece = menuService;
            _cache = cache;
        }

        public IViewComponentResult Invoke()
        {
            var childs = _menuServece.GetAllPublishMenus();

            return View(childs);
        }
    }
}
