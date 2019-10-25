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
    public class GetNewsDetailAction : CommandBase<dynamic>
    {
        public long? id { get; set; }
        protected override async Task<Result<dynamic>> ExecuteCore(ObjectContext context)
        {
            var contents = await context
                .Query
                .From("news")
                .LeftJoin("content","content.id","news.id")
                .Select("news.title","news.height","content.content_html")
                .Where("news.id", this.id)
                .Fetch<News>()
                ;
            var result = contents.FirstOrDefault();
            return await Success(new
            {
                content_html = result.content_html.Decode(),
                title = result.title.Decode(),
                hegith = result.height
            });
        }
    }
}