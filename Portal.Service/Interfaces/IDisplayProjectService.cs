using Portal.Infractructure.Utility;
using Portal.Model.Context;
using Portal.Model.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Interfaces
{
    public interface IDisplayProjectService
    {
        GetProjectWithConditionResponse GetAllProjectMatchingConditions(GetProjectWithConditionsRequest request);
        IEnumerable<portal_Projects> GetAllProject();
        portal_Projects GetDetailProject(int? projectId);
        IEnumerable<Model.Context.portal_Projects> GetRelatedProject(int? projectId);
        IEnumerable<portal_Projects> GetFeaturedProjects(int takenNumber);
    }
}
