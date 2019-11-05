using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MUP.API.Common
{
    public class DatabaseConnectService
    {
        public DatabaseConnectService()
        {
            this.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        }
        public IDbConnection Connection { get; set; }
    }
}