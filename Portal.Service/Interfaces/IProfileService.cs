using Portal.Model.Context;
using Portal.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Interfaces
{
    public interface IProfileService
    {
        IList<ProfileViewModel> GetProfiles(int pageNumber, int pageSize, out int totalItems);
        bool AddProfile(ProfileViewModel profileViewModel);
        //bool EditCMSNews(CMSNewsView cmsNewsView);
        bool DeleteProfile(Guid userId);
        //CMSNewsView GetCMSNewsById(int? newsId);
    }
}
