using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portal.Model.Context;
using Site.OnlineStore.Areas.Admin.Controllers;
using Portal.Service.Interfaces;
using Portal.Service.Implements;
using PagedList;
using Portal.Model.ViewModel;
using Site.OnlineStore.Models.ImageModels;
using System.IO;
using Portal.Service.MessageModel;
using Portal.Infractructure.Utility;
using Portal.Model.Mapper;

namespace Site.OnlineStore.Areas.Admin.Controllers
{
    public class ProjectController : BaseManagementController
    {

        #region Properties

        protected IProjectService service = new ProjectService();

        #endregion

        #region Constructures

        public ProjectController()
        {

        }

        #endregion

        #region Private functions

        /// <summary>
        /// Delete image from server
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [NonAction]
        private bool DeleteImageInFolder(string path)
        {

            string filePath = Server.MapPath("~/" + path);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Upload project image
        /// </summary>
        /// <param name="file"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public bool UploadProjectImages(HttpPostedFileBase file, out string uploadedFileName, Int32 counter = 0)
        {
            try
            {
                ImageUpload projectImage = new ImageUpload { SavePath = DisplayProjectConstants.ProjectImageFolderPath };
                var fileName = Path.GetFileName(file.FileName);
                string finalFileName = "ProjectImage_" + ((counter).ToString()) + "_" + fileName;
                if (System.IO.File.Exists(HttpContext.Request.MapPath("~" + DisplayProjectConstants.ProjectImageFolderPath + finalFileName)))
                {
                    return UploadProjectImages(file, out uploadedFileName, ++counter);
                }
                ImageResult uploadedProjectImage = projectImage.UploadProductImage(file, finalFileName, 1000);
                uploadedFileName = uploadedProjectImage.ImageName;
                return true;
            }
            catch (Exception)
            {
                uploadedFileName = null;
                return false;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Return view with list product
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var projects = service.GetProjects(page, Portal.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<ProjectSummaryViewModel> pageProducts = new StaticPagedList<ProjectSummaryViewModel>(projects, page, Portal.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageProducts);
        }

        /// <summary>
        /// Return Create view to let user input information of new project
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            PopulateStatusDropDownList();
            PopulateProjectTypeDropDownList();
            PopulateRegionDropDownList();
            PopulateProgressStatusDropDownList();
            return View();
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="productRequest">information of new project</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(CreateProjectPostRequest projectRequest)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["coverImage"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentLength > 0)
                    {
                        // width + height will force size, care for distortion
                        //Exmaple: ImageUpload imageUpload = new ImageUpload { Width = 800, Height = 700 };

                        // height will increase the width proportionally
                        //Example: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                        // width will increase the height proportionally
                        ImageUpload imageUpload = new ImageUpload { Width = 600 };

                        // rename, resize, and upload
                        //return object that contains {bool Success,string ErrorMessage,string ImageName}
                        ImageResult imageResult = imageUpload.RenameUploadFile(file);
                        if (imageResult.Success)
                        {
                            // Add new image to database
                            var photo = new share_Images
                            {
                                ImageName = imageResult.ImageName,
                                ImagePath = Path.Combine(ImageUpload.LoadPath, imageResult.ImageName)
                            };
                            var imageId = service.AddImage(photo);
                            // Add product
                            projectRequest.CoverImageId = imageId;
                            service.AddProject(projectRequest);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // use imageResult.ErrorMessage to show the error
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }
                }

            }
            PopulateProjectTypeDropDownList();
            PopulateRegionDropDownList();
            PopulateProgressStatusDropDownList();
            PopulateStatusDropDownList();
            return View(projectRequest);
        }

        /// <summary>
        /// Get information of project and return Edit View for user update data for project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portal_Projects project = service.GetProjectById((int)id);

            if (project == null)
            {
                return HttpNotFound();
            }

            // Populate status dropdownlist
            if (project.Status != null)
            {
                var status = (Define.Status)project.Status;
                PopulateStatusDropDownList(status);
            }
            else
            {
                PopulateStatusDropDownList();
            }

            // Populate project type dropdownlist
            PopulateProjectTypeDropDownList((Define.ProjectType)project.Type);

            // Populate region dropdownlist
            PopulateRegionDropDownList((Define.Region)project.Region);

            // Populate progress status dropdownlist
            PopulateProgressStatusDropDownList((Define.ProgressStatus)project.ProgressStatus);

            return View(project.ConvertToProjectFullView());
        }

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="product">information of project need to updated</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ProjectFullView project)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = service.UpdateProject(project);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            if (project.Status != null)
            {
                var status = (Define.Status)project.Status;
                PopulateStatusDropDownList(status);
            }
            else
            {
                PopulateStatusDropDownList();
            }
            // Populate project type dropdownlist
            PopulateProjectTypeDropDownList((Define.ProjectType)project.Type);

            // Populate region dropdownlist
            PopulateRegionDropDownList((Define.Region)project.Region);

            // Populate progress status dropdownlist
            PopulateProgressStatusDropDownList((Define.ProgressStatus)project.ProgressStatus);
            return View(project);
        }

        /// <summary>
        /// Upload image to server
        /// </summary>
        /// <param name="files"></param>
        /// <param name="IdProject"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> files, int projectId)
        {
            if (files != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file.ContentLength > 0)
                    {
                        string uploadedFileName = null;
                        bool isSuccess = UploadProjectImages(file, out uploadedFileName);
                        if (isSuccess)
                        {
                            share_Images largeImage = new share_Images
                            {
                                ImageName = uploadedFileName,
                                ImagePath = Path.Combine(ImageUpload.LoadPath, uploadedFileName)
                            };

                            service.AddImageForProject(projectId, largeImage);
                        }
                        else
                        {
                            // use imageResult.ErrorMessage to show the error
                            ViewBag.Error = "Upload project image fail";
                        }
                    }
                }
            }
            return ListImageProject(projectId);
        }

        /// <summary>
        /// Remove a image from list images of project
        /// </summary>
        /// <param name="Id">project Id</param>
        /// <returns>updated view of Cart</returns>
        public ActionResult DeleteImage(int projectId, int imageId)
        {
            try
            {
                share_Images deleteImages;
                bool isSuccess = service.DeleteImage(projectId, imageId, out deleteImages);
                if (isSuccess)
                {
                    //string largeImagePath = DisplayProjectConstants.ProjectImageFolderPath + deleteImages.ImageName;
                    //DeleteImageInFolder(largeImagePath);
                    DeleteImageInFolder(deleteImages.ImagePath);
                }
                else
                {
                    ViewBag.Error = "Error when delete image";
                }
                return ListImageProject(projectId);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Set image as cover image of project
        /// </summary>
        /// <param name="projectId">id of project</param>
        /// <param name="imageId">id of image</param>
        /// <returns></returns>
        public ActionResult SetAsCoverImage(int projectId, int imageId)
        {
            bool isSuccess = service.SetAsCoverImage(projectId, imageId);
            if (isSuccess)
            {
                //DeleteImageInFolder(imagePath);
            }
            else
            {
                ViewBag.Error = "Error when delete image";
            }
            return ListImageProject(projectId);
        }

        /// <summary>
        /// Delete project 
        /// </summary>
        /// <param name="id">id of project</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isSuccess = service.DeleteProject(id);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete project fail!");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// update image of product
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProjectImage(UpdateProjectImageRequest request)
        {
            if (request == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpdateProjectImage imageInfor = new UpdateProjectImage()
            {
                ImageId = request.ImageId,
                Name = request.Name,
                IsActive = request.IsActive
            };
            bool isSuccess = service.UpdateProjectImage(request.projectId, imageInfor, request.IsCoverImage);
            if (!isSuccess)
            {
                ViewBag.Error = "Error when update image";
            }
            return ListImageProject(request.projectId);
        }

        /// <summary>
        /// Create list product image for return to client side
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult ListImageProject(int projectId)
        {
            portal_Projects project = service.GetProjectById(projectId);
            ListImageProjectPartialViewModels listImageViewModels = new ListImageProjectPartialViewModels()
            {
                ProjectId = projectId,
                Images = project.share_Images.ConvertToImageProjectViewModels(),
                CoverImageId = project.CoverImageId
            };
            return PartialView("ListImageProject", listImageViewModels);
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
