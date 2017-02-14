using Portal.Infractructure.Helper;
using Portal.Infractructure.Utility;
using Portal.Model.Context;
using Portal.Model.Repository;
using Portal.Model.ViewModel;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Implements
{
    public class ProjectService : IProjectService
    {
        #region properties

        private static PortalEntities context = new PortalEntities();
        private ProjectRepository db = new ProjectRepository(context);
        private Repository<share_Images> imageRepository = new Repository<share_Images>(context);

        #endregion

        #region Constructures

        public ProjectService()
        {
            context = new PortalEntities();
            db = new ProjectRepository(context);
            imageRepository = new Repository<share_Images>(context);
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get list project with summary information
        /// </summary>
        /// <returns> Collection of summary project object </returns>
        public IEnumerable<Model.ViewModel.ProjectSummaryViewModel> GetListProjects()
        {
            IEnumerable<ProjectSummaryViewModel> listProjects = db.GetAllProjects().Select(p => new ProjectSummaryViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Type = p.Type,
                SortOrder = p.SortOrder,
                Status = EnumHelper.GetDescriptionFromEnum((Define.Status)p.Status),
                CoverImage = p.share_Images.FirstOrDefault()
            }).ToList();

            return listProjects;
        }
        /// <summary>
        /// Get list projects with paging, sort, filter
        /// </summary>
        /// <param name="pageNumber">current page index are showing on Layout</param>
        /// <param name="pageSize">total project are displayed on a page</param>
        /// <param name="totalItems">number of found project</param>
        /// <returns>List found project with summary information</returns>
        public IEnumerable<Model.ViewModel.ProjectSummaryViewModel> GetProjects(int pageNumber, int pageSize, out int totalItems)
        {
            IEnumerable<portal_Projects> projects = db.GetAllProjectsWithoutDelete();
            totalItems = projects.Count();
            IEnumerable<ProjectSummaryViewModel> returnProjectList = projects.OrderBy(b => b.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(p => new ProjectSummaryViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Type = p.Type,
                SortOrder = p.SortOrder,
                Status = EnumHelper.GetDescriptionFromEnum((Define.Status)p.Status),
                CoverImage = p.CoverImage
            }).ToList();
            return returnProjectList;
        }
        /// <summary>
        /// Add new image to database
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public int? AddImage(Model.Context.share_Images image)
        {
            try
            {
                image.Status = (int)Define.Status.Active;
                imageRepository.Insert(image);
                imageRepository.Save();
                return image.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Add new project to database
        /// </summary>
        /// <returns></returns>
        public bool AddProject(MessageModel.CreateProjectPostRequest newProject)
        {
            try
            {
                portal_Projects project = new portal_Projects()
                {
                    Name = newProject.Name,
                    Region = newProject.Region,
                    Address = newProject.Address,
                    Investor = newProject.Investor,
                    ProjectPackage = newProject.ProjectPackage,
                    TotalInvestment = newProject.TotalInvestment,
                    Duration = newProject.Duration,
                    ProgressStatus = newProject.ProgressStatus,
                    Type = newProject.Type,
                    CoverImageId = newProject.CoverImageId,
                    SortOrder = newProject.SortOrder,
                    Status = newProject.Status,
                    Description = newProject.Description,
                    Description2 = newProject.Description2
                };

                share_Images coverImage = imageRepository.GetByID(newProject.CoverImageId);
                project.share_Images.Add(coverImage);
                db.Insert(project);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Add image for exist project 
        /// </summary>
        /// <param name="ProjectId">project id</param>
        /// <param name="photo">new image</param>
        /// <param name="listImages"> return list image after adding finish</param>
        /// <returns>return true if action is success or false action is fail</returns>
        public bool AddImageForProject(int projectId, Model.Context.share_Images photo)
        {
            portal_Projects project = db.GetProjectById(projectId);
            if (project == null)
            {
                return false;
            }
            else
            {
                project.share_Images.Add(photo);
                db.Save();
                return true;
            }
        }
        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool UpdateProject(Model.ViewModel.ProjectFullView projectViewModel)
        {
            portal_Projects project = db.GetProjectById(projectViewModel.Id);
            if (project == null)
            {
                return false;
            }
            else
            {
                project.Name = projectViewModel.Name;
                project.Region = projectViewModel.Region;
                project.Address = projectViewModel.Address;
                project.Investor = projectViewModel.Investor;
                project.ProjectPackage = projectViewModel.ProjectPackage;
                project.TotalInvestment = projectViewModel.TotalInvestment;
                project.Duration = projectViewModel.Duration;
                project.ProgressStatus = projectViewModel.ProgressStatus;
                project.Type = projectViewModel.Type;
                project.SortOrder = projectViewModel.SortOrder;
                project.Status = projectViewModel.Status;
                project.Description = projectViewModel.Description;
                project.Description2 = projectViewModel.Description2;

                db.Save();
                return true;
            }
        }
        /// <summary>
        /// Get project object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Context.portal_Projects GetProjectById(int id)
        {
            return db.GetProjectById(id);
        }
        /// <summary>
        /// Delete image in project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="imageId">id of image need to delete</param>
        /// <param name="listImages">list images of project after do action</param>
        /// <param name="imagePath">path of deteled image(using for delete image in folder)</param>
        /// <returns>return true if action is success or false if action is fail</returns>
        public bool DeleteImage(int projectId, int imageId, out Model.Context.share_Images deleteImages)
        {
            try
            {
                portal_Projects project = GetProjectById(projectId);
                share_Images image = project.share_Images.Where(i => i.Id == imageId).SingleOrDefault();
                deleteImages = image;
                //Delete image in product
                project.share_Images.Remove(image);
                db.Save();
                // Delete image in table share_images
                var deleteImage = imageRepository.GetByID(imageId);
                imageRepository.Delete(deleteImage);
                imageRepository.Save();
                return true;
            }
            catch (Exception)
            {
                deleteImages = null;
                return false;
            }
        }
        /// <summary>
        /// Delete project (set Status is Delete)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteProject(int id)
        {
            try
            {
                portal_Projects project = GetProjectById(id);
                project.Status = (int)Define.Status.Delete;
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Set as cover image of project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="imageId"></param>
        /// <param name="listImages"></param>
        /// <returns></returns>
        public bool SetAsCoverImage(int projectId, int imageId)
        {
            try
            {
                portal_Projects project = GetProjectById(projectId);
                project.CoverImageId = imageId;
                db.Update(project);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// update image information of project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="image">image id</param>
        /// <param name="isCoverImage">is cover image of project or not</param>
        /// <param name="listImages">list images of project returned</param>
        /// <returns></returns>
        public bool UpdateProjectImage(int projectId, MessageModel.UpdateProjectImage imageInfor, bool isCoverImage)
        {
            try
            {
                share_Images image = imageRepository.GetByID(imageInfor.ImageId);
                image.ImageName = imageInfor.Name;
                image.Status = imageInfor.IsActive ? (int)Define.Status.Active : (int)Define.Status.Deactive;


                portal_Projects project = db.GetProjectById(projectId);
                if (isCoverImage)
                {
                    project.CoverImage = image;
                }
                else
                {
                    if (project.CoverImageId == image.Id)
                    {
                        project.CoverImage = null;
                    }
                }
                imageRepository.Update(image);
                db.Update(project);
                db.Save();
                imageRepository.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection using in repositories, which used in this service
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
            imageRepository.Dispose();
        }

        #endregion
    }
}
