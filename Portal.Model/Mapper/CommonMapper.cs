using Portal.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.Mapper
{
    public static class CommonMapper
    {
        public static IEnumerable<SearchResultViewModel> ConvertToSearchResultViewModels(this IEnumerable<MessageModel.SearchResultItem> results)
        {
            foreach (var item in results)
            {
                 yield return item.ConvertToSearchResultViewModel();
            }
        }
        public static SearchResultViewModel ConvertToSearchResultViewModel(this MessageModel.SearchResultItem item)
        {
            string path="#";
            switch (item.type)
            {
                case (int)Portal.Infractructure.Utility.Define.SearchItemType.News:
                    path = "/News/Details/" + item.Id;
                    break;
                case (int)Portal.Infractructure.Utility.Define.SearchItemType.Project:
                    path = "/Project/ProjectDetails/" + item.Id;
                    break;
            }

            SearchResultViewModel retValue = new SearchResultViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Type = item.type,
                Path = path,
                ImagePath = item.ImagePath
            };

            return retValue;
        }
    }
}
