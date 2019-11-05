using Common;
using Common.Database;
using Dapper.FastCrud;
using Entity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models
{
    public class GetNewsGroupAction : CommandBase<IEnumerable<dynamic>>
    {
        private Task<IEnumerable<NewsGroup>> GetNewsGroups(ObjectContext context)
        {
            return context.Connection.FindAsync<NewsGroup>(s=>s.Include<News>());
        }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var group = await this.GetNewsGroups(context);
            
            return await Success(group.Select(x =>
            {
                int dem = 0;
                return new
                {
                    x.Id,
                    name = x.Name,
                    sort = dem++,
                    news = x.News?.Select(n =>
                    {
                        return new
                        {
                            n.Id,
                            title = n.Title,
                            created_time = n.CreatedDate?.ToString("dd-MM-yyyy")
                        };
                    })
                };
            }));
        }
    }
}