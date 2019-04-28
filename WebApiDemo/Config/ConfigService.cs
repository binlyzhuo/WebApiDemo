using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApiDemo.Config
{
    public class ConfigService:IConfigService
    {
        

        public ConfigItem ConfigGet()
        {
            var item = new ConfigItem();
            item.ConnString = ConfigurationManager.ConnectionStrings["BaseDB"].ConnectionString;
            return item;
        }
    }

    public interface IConfigService
    {
        ConfigItem ConfigGet();
    }


}