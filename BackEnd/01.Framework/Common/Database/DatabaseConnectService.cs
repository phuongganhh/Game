using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class DatabaseConnectService
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            }
        }
        public OdbcConnection ConnectionJZ {
            get
            {
                return new OdbcConnection(ConfigurationManager.ConnectionStrings["ConnectJZ"].ConnectionString);
            }
        }
        public OdbcConnection ConnectionAccount
        {
            get
            {
                return new OdbcConnection(ConfigurationManager.ConnectionStrings["ConnectAccount"].ConnectionString);
            }
        }
        public DatabaseConnectService()
        {
        }
    }
}
