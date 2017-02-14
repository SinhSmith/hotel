using Portal.Infractructure.Utility;
using Portal.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.Repository
{
    public class ProjectRepository : Repository<portal_Projects>
    {
        #region Constructures

        public ProjectRepository(PortalEntities context)
            : base(context)
        {

        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get all project
        /// </summary>
        /// <returns></returns>
        public IEnumerable<portal_Projects> GetAllProjects()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Get projects except which one have status is Delete(Just Active and Deactive)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<portal_Projects> GetAllProjectsWithoutDelete()
        {
            return dbSet.Include("share_Images").Include("CoverImage").Where(c => c.Status != (int)Define.Status.Delete).ToList();
        }

        /// <summary>
        /// Find project by id with status not equal to Delete
        /// </summary>
        /// <returns></returns>
        public portal_Projects GetProjectById(int id)
        {
            return dbSet.Include("share_Images").Include("CoverImage").Where(c => c.Id == id && c.Status != (int)Define.Status.Delete).FirstOrDefault();
        }

        public IEnumerable<portal_Projects> GetFeaturedProjects(int takenNumber)
        {
            return dbSet.Include("CoverImage").Where(c => c.Status != (int)Define.Status.Delete).OrderBy(p => p.SortOrder).Take(takenNumber).ToList();
        }

        #endregion
    }
}
