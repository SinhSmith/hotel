using Portal.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.MessageModel
{
    public class UpdateProjectImageRequest
    {
        public int projectId { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsCoverImage { get; set; }
    }

    public class GetProjectWithConditionsRequest{
        public int Index { get; set; }
        public int NumberOfResultsPerPage { get; set; }
        public string SearchString { get; set; }
        public Nullable<int> ProjectType { get; set; }
        public Nullable<int> ProgressStatus { get; set; }
        public Nullable<int> Region { get; set; }
     }

    public class GetProjectWithConditionResponse
    {
        public int TotalNumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public int TotalProjects { get; set; }
        public IEnumerable<portal_Projects> Projects { get; set; }
    }
}
