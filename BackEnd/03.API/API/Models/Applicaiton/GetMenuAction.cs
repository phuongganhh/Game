using Common;
using Common.Database;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models
{
    public class GetMenuAction : CommandBase<IEnumerable<dynamic>>
    {
        private Task<IEnumerable<Application>> GetApplications(ObjectContext context)
        {
            return context.Query.From("application").FetchAsync<Application>();
        }
        private async Task<IEnumerable<dynamic>> GetData(ObjectContext context)
        {
            var app = await this.GetApplications(context);
            var result = app.Where(x => x.parent_id == null);
            foreach (var item in result)
            {
                item.Childrent = app.Where(x => x.parent_id.Equals(item.id));
            }
            return result.Select(x =>
            {
                return new
                {
                    name = x.name.Decode(),
                    url = x.url ?? "#",
                    style = x.style.Decode(),
                    childrent = x.Childrent?.Select(c =>
                    {
                        return new
                        {
                            name = c.name.Decode(),
                            url = c.url ?? "#",
                            style = x.style.Decode()
                        };
                    })
                };
            });
        }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            IEnumerable<dynamic> result;
            var key = "GetMenuAction";
            if (context.cache.IsSet(key))
            {
                result = context.cache.Get<IEnumerable<dynamic>>(key);
            }
            else
            {
                result = await this.GetData(context);
                context.cache.Set(key, result, 60);
            }
            return await Success(result);
        }
    }
}