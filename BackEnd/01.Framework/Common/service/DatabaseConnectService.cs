using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.service
{
    public class DatabaseConnectService
    {
        private static DatabaseConnectService _instance { get; set; }
        public static DatabaseConnectService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DatabaseConnectService();
                }
                if(_instance.Connection.State == ConnectionState.Closed)
                {
                    _instance.Connection.Open();
                }
                return _instance;
            }
        }
        public DatabaseConnectService()
        {
            this.Connection = new OdbcConnection(@"DRIVER={MySQL ODBC 3.51 Driver};SERVER=103.27.237.153;DATABASE=pa;UID=admin;PASSWORD=Thuan3;OPTION=3;");
        }
        public OdbcConnection Connection { get; set; }
    }
}
