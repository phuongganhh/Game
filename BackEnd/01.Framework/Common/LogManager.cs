
using Common.Database;
using Dapper.FastCrud;
using Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Common
{
    public class LoggerManager
    {
        private static LoggerManager _instance { get; set; }
        public static LoggerManager Instance
        {
            get
            {
                return _instance ?? (_instance = new LoggerManager());
            }
        }
        public LoggerManager()
        {
        }
        private IDbConnection Connection
        {
            get
            {
                var conn = new DatabaseConnectService().Connection;
                return conn;
            }
        }
        private async Task<long> WriteAsync(Log log,ObjectContext context = null)
        {
            if (context == null) context = new ObjectContext();
            log.UserId = context.CurrentUser?.Id;
            await this.Connection.InsertAsync<Log>(log);
            return log.Id;
        }
        private long Write(Log log,ObjectContext context = null)
        {
            if (context == null) context = new ObjectContext();
            log.UserId = context.CurrentUser?.Id;
            
            this.Connection.Insert(log);
            return log.Id;
        }
        public Task<long> ErrorAsync(Exception exception,ObjectContext context = null)
        {
            var log = new Log
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                CreatedDate = DateTime.Now,
                Type = "ERROR",
                Param = null
            };
            return this.WriteAsync(log, context);
        }
        public long Error(Exception exception,ObjectContext context = null)
        {
            var log = new Log
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                CreatedDate = DateTime.Now,
                Type = "ERROR",
                Param = null
            };
            return this.Write(log, context);
        }
        public Task<long> InfoAsync(string message,string param = null,ObjectContext context = null)
        {
            var log = new Log
            {
                Message = message,
                CreatedDate = DateTime.Now,
                Type = "INFO",
                Param = param
            };
            return this.WriteAsync(log, context);
        }
        public long Info(string message,string param = null,ObjectContext context = null)
        {
            var log = new Log
            {
                Message = message,
                CreatedDate = DateTime.Now,
                Type = "INFO",
                Param = param
            };
            return this.Write(log, context);
        }

        public Task<long> TraceAsync(string message,string param = null,ObjectContext context = null)
        {
            var log = new Log
            {
                CreatedDate = DateTime.Now,
                Message = message,
                StackTrace = null,
                Type = "TRACE",
                Param = param
            };
            return this.WriteAsync(log, context);
        }

        public long Trace(string message,string param = null,ObjectContext context = null)
        {
            var log = new Log
            {
                CreatedDate = DateTime.Now,
                Message = message,
                StackTrace = null,
                Type = "TRACE",
                Param = param
            };
            return this.Write(log, context);
        }
    }
}
