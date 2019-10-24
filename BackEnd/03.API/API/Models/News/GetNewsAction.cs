using Common;
using Common.Database;
using Common.Framework;
using Dapper;
using Dapper.FastCrud;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models
{
    public class GetNewsAction : CommandBase<IEnumerable<dynamic>>
    {
        public int? current_page { get; set; }
        public int? page_size { get; set; }
        public long? total_page { get; set; }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var result = await this.GetNewsGroup(context);
            foreach (var item in result)
            {
                item.News = await this.GetNews(context, item.id);
                foreach (var n in item.News)
                {
                    n.title = n.title.Decode();
                }
                item.TotalRecord = await this.GetTotal(context, item.id);
            }
            return await Success(result);
        }
        protected override Task ValidateCore(ObjectContext context)
        {
            this.current_page = this.current_page ?? 1;
            this.page_size = this.page_size ?? 16;
            return Task.CompletedTask;
        }
        private Task<int> GetTotal(ObjectContext context, string group_id)
        {
            var sql = $"SELECT COUNT(*) FROM news WHERE news.news_group_id = '{group_id}'";
            return context.Connection.ExecuteScalarAsync<int>(sql);
        }
        private Task<IEnumerable<News>> GetNews(ObjectContext context,string group_id)
        {
            return context.Query.From("news").Where("news_group_id",group_id).ForPage(this.current_page.Value, this.page_size.Value).Get<News>();
        }
        private Task<IEnumerable<NewsGroup>> GetNewsGroup(ObjectContext context)
        {
            return context.Query.From("news_group").Get<NewsGroup>();
        }
    }
}