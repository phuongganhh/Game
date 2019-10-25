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
            return context.Query.From("application").Fetch<Application>();
        }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var app = await this.GetApplications(context);
            var result = app.Where(x => x.parent_id == null);
            foreach (var item in result)
            {
                item.Childrent = app.Where(x => x.parent_id.Equals(item.id));
            }
            return await Success(result.Select(x => {
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
            }));
        }
    }
}