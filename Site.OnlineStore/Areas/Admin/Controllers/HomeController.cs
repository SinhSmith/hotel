using Portal.Service.Implements;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.OnlineStore.Areas.Admin.Controllers
{
    public class HomeController : BaseManagementController
    {
        #region Properties

        private IMenuService _menuService = new MenuService();

        #endregion

        #region Actions
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var menu = _menuService.GetMenuByType((int)Portal.Infractructure.Utility.Define.MenuEnum.Admin);
            return PartialView(menu);
        }

        #endregion
    }
}
