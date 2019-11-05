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
    public class GetNewsDetailAction : CommandBase<dynamic>
    {
        public long id { get; set; }
        protected override async Task<Result<dynamic>> ExecuteCore(ObjectContext context)
        {
            var result = await context
                .Connection
                .FindAsync<News>(s=>s.Where($"NewsDetailId = @id").WithParameters(new { this.id}).Include<NewsDetail>())
                ;
            var content = result.FirstOrDefault();
            return await Success(new
            {
                content_html = content.NewsDetail?.ContentHtml,
                title = content.Title
            });
        }
    }
}