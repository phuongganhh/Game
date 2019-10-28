using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Settings
    {
        private static Settings _instance { get; set; }
        public static Settings Instance
        {
            get
            {
                return _instance ?? (_instance = new Settings());
            }
        }

        public string FontEnd
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("front_end");
            }
        }
    }
}
