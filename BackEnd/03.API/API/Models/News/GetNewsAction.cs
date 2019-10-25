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
        public long? news_group_id { get; set; }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var data = await context.Query.From("news").Where("news.news_group_id", this.news_group_id).ForPage(this.current_page.Value,this.page_size.Value).Fetch<News>();
            var total = await this.GetTotal(context);
            return await Success(data.Select(x=> {
                return new
                {
                    title = x.title.Decode(),
                    id = x.id,
                    created_time = new DateTime(x.created_time.Value).ToString("dd-MM-yyyy HH:mm:ss")
                };
            }),new Paging {
                current_page = this.current_page,
                page_size = this.page_size,
                total = total
            });
        }
        protected override Task ValidateCore(ObjectContext context)
        {
            this.current_page = this.current_page ?? 1;
            this.page_size = this.page_size ?? 16;
            return Task.CompletedTask;
        }
        private Task<int> GetTotal(ObjectContext context)
        {
            var sql = $"SELECT COUNT(*) FROM news WHERE news.news_group_id = '{this.news_group_id}'";
            return context.Connection.ExecuteScalarAsync<int>(sql);
        }
        
    }
}