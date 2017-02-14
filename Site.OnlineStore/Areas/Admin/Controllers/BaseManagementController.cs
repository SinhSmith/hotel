using Portal.Infractructure.Helper;
using Portal.Infractructure.Utility;
using Portal.Service.Implements;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.OnlineStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BaseManagementController : Controller
    {
        #region Properties

        #endregion

        #region Constructures

        public BaseManagementController()
        {
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get list options for Status dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="status"></param>
        protected virtual void PopulateStatusDropDownList(Define.Status status = Define.Status.Active)
        {
            IEnumerable<Define.Status> values = Enum.GetValues(typeof(Define.Status)).Cast<Define.Status>();
            IEnumerable<SelectListItem> items = from value in values
                                                where value != Define.Status.Delete
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.Status)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == status,
                                                };

            ViewBag.Status = items;
        }

        /// <summary>
        /// Get list options for Region dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="status"></param>
        protected virtual void PopulateRegionDropDownList(Define.Region region = Define.Region.North)
        {
            IEnumerable<Define.Region> values = Enum.GetValues(typeof(Define.Region)).Cast<Define.Region>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.Region)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == region,
                                                };

            ViewBag.Region = items;
        }

        /// <summary>
        /// Get list options for progress status dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="progressStatus"></param>
        protected virtual void PopulateProgressStatusDropDownList(Define.ProgressStatus progressStatus = Define.ProgressStatus.InProgress)
        {
            IEnumerable<Define.ProgressStatus> values = Enum.GetValues(typeof(Define.ProgressStatus)).Cast<Define.ProgressStatus>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.ProgressStatus)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == progressStatus,
                                                };

            ViewBag.ProgressStatus = items;
        }

        /// <summary>
        /// Get list options for project type dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="projectType"></param>
        protected virtual void PopulateProjectTypeDropDownList(Define.ProjectType projectType = Define.ProjectType.DesignAndConstruction)
        {
            IEnumerable<Define.ProjectType> values = Enum.GetValues(typeof(Define.ProjectType)).Cast<Define.ProjectType>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.ProjectType)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == projectType
                                                };

            ViewBag.Type = items;
        }

        /// <summary>
        /// Get list options for banner types dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="status"></param>
        protected virtual void PopulateBannerTypesDropDownList(Define.BannerTypes banner = Define.BannerTypes.SpringSeason)
        {
            IEnumerable<Define.BannerTypes> values = Enum.GetValues(typeof(Define.BannerTypes)).Cast<Define.BannerTypes>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.BannerTypes)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == banner,
                                                };

            ViewBag.Type = items;
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