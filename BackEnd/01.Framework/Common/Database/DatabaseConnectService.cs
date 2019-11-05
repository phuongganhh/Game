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
        public IDbConnection Connection { get; set; }
        public OdbcConnection ConnectionJZ { get; set; }
        public OdbcConnection ConnectionAccount { get; set; }
        public DatabaseConnectService()
        {
            this.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            this.ConnectionJZ = new OdbcConnection(ConfigurationManager.ConnectionStrings["ConnectJZ"].ConnectionString);
            this.ConnectionAccount = new OdbcConnection(ConfigurationManager.ConnectionStrings["ConnectAccount"].ConnectionString);
        }
    }
}
