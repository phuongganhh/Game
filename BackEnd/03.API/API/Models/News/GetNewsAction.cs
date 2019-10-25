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
            var data = await context.Query.From("content").Fetch<Content>();
            return await Success(data);
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
        
    }
}