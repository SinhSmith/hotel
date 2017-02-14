using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.MessageModel
{
    public class SearchResultItem
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int type { get; set; }
        public string ImagePath { get; set; }
    }

    public class SearchDataResponse
    {
        public IEnumerable<SearchResultItem> Results { get; set; }
        public int TotalResult { get; set; }
    }
}
