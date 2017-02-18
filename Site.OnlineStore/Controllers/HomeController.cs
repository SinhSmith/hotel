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
using System.Threading.Tasks;
using OnlineStoreMVC.Models;
using System.Net;
using System.Net.Mail;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("trungkien3289@gmail.com")); //replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
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
                case (int)Portal.Infractructure.Utility.Define.HotelServiceType.Room:
                    {
                        return View("AccomodationIntroduction");
                    }
                case (int)Portal.Infractructure.Utility.Define.HotelServiceType.Spa:
                    {
                        return View("SpaIntroduction");
                    }
                case (int)Portal.Infractructure.Utility.Define.HotelServiceType.Tour:
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