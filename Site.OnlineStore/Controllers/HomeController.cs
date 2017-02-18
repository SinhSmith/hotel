using PagedList;
using Portal.Model.ViewModel;
using Portal.Service.Implements;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Infractructure.Utility;
using static Portal.Infractructure.Utility.Define;

namespace Site.OnlineStore.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties

        IBannerService _bannerService = new BannerService();
        ICMSNewsService _cmsNewsService = new CMSNewsService();
        IDisplayProjectService _projectService = new DisplayProjectService();

        #endregion

        #region Constructures

        public HomeController()
        {
            _bannerService = new BannerService();
            _cmsNewsService = new CMSNewsService();
            _projectService = new DisplayProjectService();
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            ViewBag.FeaturedProjects = _projectService.GetFeaturedProjects(6).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Rooms()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _HeaderPartial()
        {
            return PartialView();
        }

        public ActionResult BannerPartial()
        {
            return PartialView(_bannerService.GetBanners1ForHomePage());
        }

        public ActionResult BlogPartial()
        {
            return PartialView(_cmsNewsService.GetCMSNewsForHomePage());
        }

        public ActionResult Search(string keyword = "",int? page = 1)
        {
            ViewBag.SearchString = keyword;
            ViewBag.FeaturedProjects = _projectService.GetFeaturedProjects(6).ToList();
            SearchResultResponse result = SeachData(keyword, page);
            IPagedList<SearchResultViewModel> pageProjects = new StaticPagedList<SearchResultViewModel>(result.Items, (int)page, Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE, result.TotalItems);
            return View("Search", pageProjects);
        }

        public ActionResult ServiceDetails(int id)
        {
            switch (id)
            {
                case (int)HotelServiceType.Room:
                    {
                        return View("AccomodationIntroduction");
                    }
                case (int)HotelServiceType.Spa:
                    {
                        return View("SpaIntroduction");
                    }
                case (int)HotelServiceType.Tour:
                    {
                        return View("TourIntroduction");
                    }
                default: return View("Services");
                    
            }
        }

        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
    }
}