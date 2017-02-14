using Portal.Model.MessageModel;
using Portal.Model.ViewModel;
using Portal.Service.Implements;
using Portal.Service.Interfaces;
using Portal.Service.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Model.Mapper;

namespace Site.OnlineStore.Controllers
{
    public class BaseController : Controller
    {
        #region Properties

        protected ICommonService _service;

        #endregion
        
        #region Constructures

        public BaseController()
        {
            _service = new CommonService();
        }

        #endregion

        #region Public functions
        public SearchResultResponse SeachData(string keyword, int? page)
        {
            SearchDataResponse result = _service.Search(keyword, (int)page);
            SearchResultResponse retValue = new SearchResultResponse()
            {
                Items = result.Results.ConvertToSearchResultViewModels(),
                TotalItems = result.TotalResult
            };
            return retValue;
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