using Portal.Infractructure.Utility;
using Portal.Model.Context;
using Portal.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.Mapper
{
    public static class ProjectMapper
    {
        public static ProjectFullView ConvertToProjectFullView(this portal_Projects project)
        {
            ProjectFullView projectFullView = new ProjectFullView()
            {
                Id = project.Id,
                Name = project.Name,
                Region = project.Region,
                Address = project.Address,
                Investor = project.Investor,
                ProjectPackage = project.ProjectPackage,
                CoverImageId = project.CoverImageId,
                TotalInvestment = project.TotalInvestment,
                Duration = project.Duration,
                ProgressStatus = project.ProgressStatus,
                Type = project.Type,
                SortOrder = project.SortOrder,
                Status = project.Status,
                Description = project.Description,
                Description2 = project.Description2,
                share_Images = project.share_Images.ConvertToImageProjectViewModels()
            };

            return projectFullView;
        }

        public static ImageProjectViewModel ConvertToImageProjectViewModel(this share_Images image)
        {
            ImageProjectViewModel imageViewModel = new ImageProjectViewModel()
            {
                Id = image.Id,
                ImageName = image.ImageName,
                ImagePath = image.ImagePath,
                IsActive = image.Status == (int)Define.Status.Active ? true : false
            };

            return imageViewModel;
        }

        public static IEnumerable<ImageProjectViewModel> ConvertToImageProjectViewModels(this IEnumerable<share_Images> images)
        {
            IList<ImageProjectViewModel> listImages = new List<ImageProjectViewModel>();
            foreach (share_Images image in images)
            {
                listImages.Add(image.ConvertToImageProjectViewModel());
            }

            return listImages;
        }
    }
}
