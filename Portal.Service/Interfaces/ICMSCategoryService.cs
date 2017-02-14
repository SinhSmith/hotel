using Portal.Model.Context;
using Portal.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Interfaces
{
    public interface ICMSCategoryService
    {
        IList<CMSCategoryView> GetCMSCategories(int pageNumber, int pageSize, out int totalItems);
        bool AddCMSCategory(CMSCategoryView categoryView);
        bool EditCMSCategory(CMSCategoryView categoryView);
        bool DeleteCMSCategory(int id);
        CMSCategoryView GetCategoryById(int? categoryId);
        IList<CMSCategoryView> GetChildCategoriesByParentId(int? parentId);
    }
}
