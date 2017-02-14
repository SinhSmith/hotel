using Portal.Model.Context;
using Portal.Model.ViewModel;
using Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.Implements
{
    public class MenuService : IMenuService
    {
        public IList<MenuView> GetMenuByType(int typeId)
        {
            using (var db = new PortalEntities())
            {
                return db.system_Menu.Where(x => x.Type == typeId && x.Status == (int)Portal.Infractructure.Utility.Define.Status.Active).OrderBy(x => x.SortOrder)
                    .Select(x => new MenuView
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Url = x.Url,
                        Type = x.Type,
                        Icon = x.Icon
                    }).ToList();
            }
        }
    }
}
