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
            return context.Query.From("news_group").FetchAsync<NewsGroup>();
        }
        private Task<IEnumerable<News>> GetNews(ObjectContext context,NewsGroup newsGroup)
        {
            return context.Query.From("news").Where("news.news_group_id", newsGroup.id).FetchAsync<News>();
        }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var group = await this.GetNewsGroups(context);
            var dem = 0;
            foreach (var item in group)
            {
                item.News = await this.GetNews(context, item);
            }
            return await Success(group.Select(x =>
            {
                return new
                {
                    x.id,
                    name = x.name.Decode(),
                    sort = dem++,
                    news = x.News?.Select(n =>
                    {
                        return new
                        {
                            n.id,
                            title = n.title.Decode(),
                            created_time = new DateTime(n.created_time.Value).ToString("dd-MM-yyyy")
                        };
                    })
                };
            }));
        }
    }
}