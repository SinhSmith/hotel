using Portal.Model.MessageModel;
using Portal.Service.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Interfaces
{
    public interface ICommonService
    {
        SearchDataResponse Search(string keyword = "", int page = 0);
    }
}
