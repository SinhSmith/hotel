using Portal.Model.Context;
using Portal.Model.ViewModel;
using Portal.Service.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectSummaryViewModel> GetListProjects();
        IEnumerable<ProjectSummaryViewModel> GetProjects(int pageNumber, int pageSize, out int totalItems);

        /// <summary>
        /// Add new image to database
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Nullable<int> AddImage(share_Images image);
        /// <summary>
        /// Add new product to database
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        bool AddProject(CreateProjectPostRequest newProject);
        /// <summary>
        /// Add image for exist product 
        /// </summary>
        /// <param name="IdProduct">product id</param>
        /// <param name="photo">new image</param>
        /// <param name="listImages"> return list image after adding finish</param>
        /// <returns>return true if action is success or false action is fail</returns>
        bool AddImageForProject(int projectId, share_Images photo);
        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool UpdateProject(ProjectFullView projectViewModel);
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        portal_Projects GetProjectById(int id);
        /// <summary>
        /// Delete image in product
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="imageId">id of image need to delete</param>
        /// <param name="listImages">list images of product after do action</param>
        /// <param name="imagePath">path of deteled image(using for delete image in folder)</param>
        /// <returns>return true if action is success or false if action is fail</returns>
        bool DeleteImage(int projectId, int imageId, out share_Images deleteImages);
        /// <summary>
        /// Delete product (set status is Delete)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool DeleteProject(int id);
        /// <summary>
        /// Set as cover image of product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <param name="listImages"></param>
        /// <returns></returns>
        bool SetAsCoverImage(int projectId, int imageId);
        /// <summary>
        /// update image information of product
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="image">image id</param>
        /// <param name="isCoverImage">is cover image of product or not</param>
        /// <param name="listImages">list images of product returned</param>
        /// <returns></returns>
        bool UpdateProjectImage(int projectId, UpdateProjectImage imageInfor, bool isCoverImage);
    }
}
