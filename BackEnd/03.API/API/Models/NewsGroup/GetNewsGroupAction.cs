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
    public class GetNewsGroupAction : CommandBase<IEnumerable<dynamic>>
    {
        private Task<IEnumerable<NewsGroup>> GetNewsGroups(ObjectContext context)
        {
            return context.Query.From("news_group").Gets<NewsGroup>();
        }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var group = await this.GetNewsGroups(context);
            return await Success(group.Select(x =>
            {
                return new
                {
                    x.id,
                    name = x.name.Decode()
                };
            }));
        }
    }
}