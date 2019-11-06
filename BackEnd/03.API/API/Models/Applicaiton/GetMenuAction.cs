using Common;
using Common.Database;
using Dapper.FastCrud;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class GetMenuAction : CommandBase<IEnumerable<dynamic>>
    {
        private Task<IEnumerable<Application>> GetApplications(ObjectContext context)
        {
            return context.Connection.FindAsync<Application>();
        }
        private async Task<IEnumerable<dynamic>> GetData(ObjectContext context)
        {
            var app = await this.GetApplications(context);
            return app.Select(x =>
            {
                return new
                {
                    name = x.Name,
                    url = x.Url ?? "#",
                    style = x.Style,
                    childrent = app.Where(a => a.ParentId == x.Id).Select(c =>
                    {
                        return new
                        {
                            name = c.Name,
                            url = c.Url ?? "#",
                            style = x.Style
                        };
                    })
                };
            });
        }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            IEnumerable<dynamic> result;
            var key = "GetMenuAction";
            if (context.Cache.IsSet(key))
            {
                result = context.Cache.Get<IEnumerable<dynamic>>(key);
            }
            else
            {
                result = await this.GetData(context);
                context.Cache.Set(key, result, 60);
            }
            return await Success(result);
        }
    }
}