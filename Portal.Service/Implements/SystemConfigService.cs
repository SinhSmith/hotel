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
    public class SystemConfigService : ISystemConfigService
    {
        public int GetTotalVisitors()
        {
            using (var db = new PortalEntities())
            {
                var config = db.system_Config.FirstOrDefault(x => x.Name == Portal.Infractructure.Utility.Define.SystemConfig.TotalVisitors.ToString());
                if (config != null)
                {
                    return Convert.ToInt32(config.Value);
                }
                return 0;
            }
        }

        public bool UpdateTotalVisitors()
        {
            try
            {
                using (var db = new PortalEntities())
                {
                    var config = db.system_Config.FirstOrDefault(x => x.Name == Portal.Infractructure.Utility.Define.SystemConfig.TotalVisitors.ToString());
                    if (config != null)
                    {
                        config.Value = (Convert.ToInt32(config.Value) + 1).ToString();
                    }
                    else
                    {
                        var TrackingConfig = new system_Config
                        {
                            Name = Portal.Infractructure.Utility.Define.SystemConfig.TotalVisitors.ToString(),
                            Value = "1",
                            Status = (int)Portal.Infractructure.Utility.Define.Status.Active
                        };
                        db.system_Config.Add(TrackingConfig);
                    }
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
