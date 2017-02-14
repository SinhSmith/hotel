using Portal.Model.Context;
using Portal.Model.MessageModel;
using Portal.Service.Interfaces;
using Portal.Service.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Implements
{
    public class CommonService : ICommonService
    {
        #region Properties

        private static PortalEntities context = new PortalEntities();

        #endregion

        #region Constructors
        public CommonService()
        {
            context = new PortalEntities();
        }
        #endregion

        #region Public Functions
        public SearchDataResponse Search(string keyword = "", int page = 1)
        {
            IEnumerable<SearchResultItem> results = context.Database.SqlQuery<SearchResultItem>(
                    "SELECT Title as Title,[cms_News].Id,ImagePath, 0 as 'Type'" +
                    " FROM   cms_News inner join share_Images on cms_News.CoverImageId = share_Images.Id" +
                    " WHERE  Title like '%" + keyword + "%'" +
                    " UNION" +
                    " SELECT Name as Title,[portal_Projects].Id,ImagePath, 1 as 'Type'" +
                    " FROM   portal_Projects inner join share_Images on portal_Projects.CoverImageId = share_Images.Id" +
                    " WHERE  Name like '%" + keyword + "%'"
                    ).Skip((page-1)*8).Take(8).ToList().Cast<SearchResultItem>();
            int total = context.Database.SqlQuery<int>(
                    "SELECT COUNT(*) "+
                    " FROM("+
                    " SELECT Title as Title,[cms_News].Id,ImagePath, 0 as 'Type'" +
                    " FROM   cms_News inner join share_Images on cms_News.CoverImageId = share_Images.Id" +
                    " WHERE  Title like '%" + keyword + "%'" +
                    " UNION" +
                    " SELECT Name as Title,[portal_Projects].Id,ImagePath, 1 as 'Type'" +
                    " FROM   portal_Projects inner join share_Images on portal_Projects.CoverImageId = share_Images.Id" +
                    " WHERE  Name like '%" + keyword + "%'"+
                    " )src"
                    ).First();
            SearchDataResponse response = new SearchDataResponse()
            {
                TotalResult = total,
                Results = results
            };
            return response;
        }
        #endregion

    }
}
