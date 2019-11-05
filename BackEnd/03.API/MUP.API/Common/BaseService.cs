using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MUP.API.Common
{
    public class BaseService
    {
        private DatabaseConnectService Connector { get; set; }
        public BaseService()
        {
            this.Connector = new DatabaseConnectService();
        }
        public IDbConnection Connection
        {
            get
            {
                return this.Connector.Connection;
            }
        }
    }
}