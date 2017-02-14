using Portal.Model.Context;
using Portal.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Interfaces
{
    public interface IBannerService
    {
        IList<BannerViewModel> GetBanners1ForHomePage();
        BannerViewModel GetBanners2ForHomePage();
        BannerViewModel GetActivePopupForHomePage();
        IList<BannerViewModel> GetBanners(int pageNumber, int pageSize, out int totalItems);
        bool AddBanner(BannerViewModel bannerViewModel);
        bool EditBanner(BannerViewModel bannerViewModel);
        bool DeleteBanner(int id);
        BannerViewModel GetBannerById(int? id);
    }
}
