using LinqKit;
using Portal.Model.Context;
using Portal.Model.MessageModel;
using Portal.Model.Repository;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Implements
{
    public class DisplayProjectService : IDisplayProjectService
    {
        #region Properties

        private static PortalEntities context = new PortalEntities();
        ProjectRepository db = new ProjectRepository(context);

        #endregion

        #region Constructures

        public DisplayProjectService()
        {
            context = new PortalEntities();
            db = new ProjectRepository(context);
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Get list project with conditions and paging
        /// </summary>
        /// <param name="request">conditions for filter</param>
        /// <returns>list project matching with conditions</returns>
        private IEnumerable<portal_Projects> GetMatchingConditionProjects(GetProjectWithConditionsRequest request)
        {
            var searchQuery = PredicateBuilder.True<portal_Projects>();

            if (request.ProjectType != null)
            {
                searchQuery = searchQuery.And(p => p.Type == (int)request.ProjectType);
            }
            if (request.Region != null)
            {
                searchQuery = searchQuery.And(p => p.Region == (int)request.Region);
            }
            if (request.ProgressStatus != null)
            {
                searchQuery = searchQuery.And(p => p.ProgressStatus == (int)request.ProgressStatus);
            }
            if (request.SearchString != null && request.SearchString != string.Empty)
            {
                searchQuery = searchQuery.And(p => p.Name.Contains(request.SearchString));
            }

            IEnumerable<portal_Projects> projectsMatchingRefinement = db.Get(
                filter: searchQuery, includeProperties: "share_Images,cms_News,CoverImage");

            return projectsMatchingRefinement.ToList();
        }

        /// <summary>
        /// Get list project with conditions except selected project
        /// </summary>
        /// <param name="projectId">selected project id</param>
        /// <param name="request">conditions for filter</param>
        /// <returns>list project matching with conditions</returns>
        private IEnumerable<portal_Projects> GetRelatedProjects(int projectId,GetProjectWithConditionsRequest request)
        {
            var searchQuery = PredicateBuilder.True<portal_Projects>();

            if (request.ProjectType != null)
            {
                searchQuery = searchQuery.And(p => p.Type == (int)request.ProjectType);
            }
            if (request.Region != null)
            {
                searchQuery = searchQuery.And(p => p.Region == (int)request.Region);
            }
            if (request.ProgressStatus != null)
            {
                searchQuery = searchQuery.And(p => p.ProgressStatus == (int)request.ProgressStatus);
            }
            if (request.SearchString != null && request.SearchString != string.Empty)
            {
                searchQuery = searchQuery.And(p => p.Name.Contains(request.SearchString));
            }

            searchQuery = searchQuery.And(p => p.Id != projectId);

            IEnumerable<portal_Projects> projectsMatchingRefinement = db.Get(
                filter: searchQuery, includeProperties: "share_Images,cms_News,CoverImage");

            return projectsMatchingRefinement.ToList();
        }

        /// <summary>
        /// Crop list project result to satisfy current page
        /// </summary>
        /// <param name="productsFound">list project need to paging</param>
        /// <param name="pageIndex">current index of page on Layout</param>
        /// <param name="numberOfResultsPerPage">total project displayed on a page</param>
        /// <returns>list project result</returns>
        private IEnumerable<portal_Projects> CropProjectListToSatisfyGivenIndex(IEnumerable<portal_Projects> projectsFound, int pageIndex, int numberOfResultsPerPage)
        {
            if (pageIndex > 1)
            {
                int numToSkip = (pageIndex - 1) * numberOfResultsPerPage;
                return projectsFound.Skip(numToSkip)
                .Take(numberOfResultsPerPage).ToList();
            }
            else
            {
                return projectsFound.Take(numberOfResultsPerPage).ToList();
            }
        }

        #endregion

        #region Public functions

        public IEnumerable<portal_Projects> GetAllProject()
        {
            return db.GetAllProjectsWithoutDelete();
        }

        public GetProjectWithConditionResponse GetAllProjectMatchingConditions(GetProjectWithConditionsRequest request)
        {
            IEnumerable<portal_Projects> foundProjects = GetMatchingConditionProjects(request);
            GetProjectWithConditionResponse response = new GetProjectWithConditionResponse()
            {
                CurrentPage = request.Index,
                SearchString = request.SearchString,
                TotalNumberOfPages = (int)Math.Ceiling((double)foundProjects.Count() / request.NumberOfResultsPerPage),
                TotalProjects = foundProjects.Count(),
                Projects = CropProjectListToSatisfyGivenIndex(foundProjects, request.Index, request.NumberOfResultsPerPage)
            };

            return response;
        }

        public Model.Context.portal_Projects GetDetailProject(int? projectId)
        {
            return db.GetProjectById((int)projectId);
        }

        public IEnumerable<Model.Context.portal_Projects> GetRelatedProject(int? projectId)
        {
            try
            {
                var project = db.GetProjectById((int)projectId);
                if (project == null)
                {
                    return null;
                }

                GetProjectWithConditionsRequest request = new GetProjectWithConditionsRequest()
                {
                    ProgressStatus = project.ProgressStatus,
                    ProjectType = project.Type,
                    Region = project.Region
                };

                return GetRelatedProjects((int)projectId, request).Take(3);
            }
            catch (Exception)
            {
                return null;
                // Log exceptions
            }
        }

        public IEnumerable<portal_Projects> GetFeaturedProjects(int takenNumber)
        {
            return db.GetFeaturedProjects(takenNumber);
        }

        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection using in repositories, which used in this service
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }

        #endregion
    }
}
