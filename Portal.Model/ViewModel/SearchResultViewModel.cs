using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.ViewModel
{
    public class SearchResultViewModel
    {
        public int Id { get; set; }
        public string Title{get;set;}
        public int Type { get; set; }
        public string Path { get; set; }
        public string ImagePath { get; set; }
    }
    public class SearchResultResponse
    {
        public IEnumerable<SearchResultViewModel> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
