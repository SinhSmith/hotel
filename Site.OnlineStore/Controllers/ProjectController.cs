using PagedList;
using Portal.Infractructure.Helper;
using Portal.Infractructure.Utility;
using Portal.Model.Context;
using Portal.Model.MessageModel;
using Portal.Service.Implements;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Site.OnlineStore.Controllers
{
    public class ProjectController : Controller
    {
        #region Private Properties

        private IDisplayProjectService service;

        #endregion

        #region Constructures

        public ProjectController(){
            service = new DisplayProjectService();
        }

        #endregion

        #region Actions

        public ActionResult Index(string searchString = "", int page = 1)
        {
            GetProjectWithConditionsRequest request = new GetProjectWithConditionsRequest()
            {
                Index = page,
                NumberOfResultsPerPage = Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE,
                ProgressStatus = null,
                ProjectType = null,
                Region = null,
                SearchString = searchString
            };

            GetProjectWithConditionResponse response = service.GetAllProjectMatchingConditions(request);

            IPagedList<portal_Projects> pageProjects = new StaticPagedList<portal_Projects>(response.Projects, page, Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE, response.TotalProjects);

            ViewBag.Category = "All Project";
            ViewBag.CategoryValue = null;
            ViewBag.CategoryType = Portal.Infractructure.Utility.Define.CategoryType.All;
            return View("DisplayProjects", pageProjects);
        }

        public ActionResult GetProjectByType(int? type = null, string searchString = "", int page = 1)
        {

            GetProjectWithConditionsRequest request = new GetProjectWithConditionsRequest()
            {
                Index = page,
                NumberOfResultsPerPage = Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE,
                ProgressStatus = null,
                ProjectType = type,
                Region = null,
                SearchString = searchString
            };

            GetProjectWithConditionResponse response = service.GetAllProjectMatchingConditions(request);

            IPagedList<portal_Projects> pageProjects = new StaticPagedList<portal_Projects>(response.Projects, page, Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE, response.TotalProjects);

            if (type!=null)
            {
                ViewBag.Category = EnumHelper.GetDescriptionFromEnum((Define.ProjectType)type);
                ViewBag.CategoryValue = type;
            }
            else
            {
                ViewBag.Category = "All";
                ViewBag.CategoryValue = null;
            }
            ViewBag.CategoryType = Portal.Infractructure.Utility.Define.CategoryType.ProjectType;
            return View("DisplayProjects", pageProjects);
        }

        public ActionResult GetProjectByRegion(int? region = null, string searchString = "", int page = 1)
        {

            GetProjectWithConditionsRequest request = new GetProjectWithConditionsRequest()
            {
                Index = page,
                NumberOfResultsPerPage = Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE,
                ProgressStatus = null,
                ProjectType = null,
                Region = region,
                SearchString = searchString
            };

            GetProjectWithConditionResponse response = service.GetAllProjectMatchingConditions(request);
            IPagedList<portal_Projects> pageProjects = new StaticPagedList<portal_Projects>(response.Projects, page, Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE, response.TotalProjects);
            if (region != null)
            {
                ViewBag.Category = EnumHelper.GetDescriptionFromEnum((Define.Region)region);
                ViewBag.CategoryValue = region;
            }
            else
            {
                ViewBag.Category = "All";
                ViewBag.CategoryValue = null;
            }
            ViewBag.CategoryType = Portal.Infractructure.Utility.Define.CategoryType.Region;
            return View("DisplayProjects", pageProjects);
        }

        public ActionResult GetProjectByProgressStatus(int? progressStatus = null, string searchString = "", int page = 1)
        {

            GetProjectWithConditionsRequest request = new GetProjectWithConditionsRequest()
            {
                Index = page,
                NumberOfResultsPerPage = Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE,
                ProgressStatus = progressStatus,
                ProjectType = null,
                Region = null,
                SearchString = searchString
            };

            GetProjectWithConditionResponse response = service.GetAllProjectMatchingConditions(request);
            IPagedList<portal_Projects> pageProjects = new StaticPagedList<portal_Projects>(response.Projects, page, Portal.Infractructure.Utility.Define.DISPLAY_PROJECT_PAGE_SIZE, response.TotalProjects);
            if (progressStatus != null)
            {
                ViewBag.Category = EnumHelper.GetDescriptionFromEnum((Define.ProgressStatus)progressStatus);
                ViewBag.CategoryValue = progressStatus;
            }
            else
            {
                ViewBag.Category = "All";
                ViewBag.CategoryValue = null;
            }
            ViewBag.CategoryType = Portal.Infractructure.Utility.Define.CategoryType.ProgressStatus;

            return View("DisplayProjects", pageProjects);
        }

        public ActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            portal_Projects response = service.GetDetailProject(id);
            ViewBag.ProjectType = EnumHelper.GetDescriptionFromEnum((Define.ProjectType)response.Type);
            ViewBag.Region = EnumHelper.GetDescriptionFromEnum((Define.Region)response.Region);
            ViewBag.ProgressStatus = EnumHelper.GetDescriptionFromEnum((Define.ProgressStatus)response.ProgressStatus);
            ViewBag.RelatedProjects = service.GetRelatedProject(id);
            return View("ProjectDetails", response);
        }

        #endregion
    }
}